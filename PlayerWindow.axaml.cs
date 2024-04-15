using System.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using SuperVolleyball.Entity;

namespace SuperVolleyball;

public partial class PlayerWindow : Window
{
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    private AvaloniaList<Player> _players;
    private AvaloniaList<Player> _playerPreSearch;
    private AvaloniaList<Team> _teams;
    private AvaloniaList<Position> _positions;
    private AvaloniaList<Position> _positionPreSearchn;
    private MySqlConnection _connection;

    public TournamentWindow()
    {
        InitializeComponent();
        ShowPlayerTable();
    }

    public void ShowPlayerTable()
    {
        MySqlConnection _connection;
        string sql =
            "select Player.ID, Player.Name, p.Name as Position, Player.Weight, Player.Height, Player.DateOfBirth, Player.DateOfStartMatch, t.Name as Team  " +
            "from pro1_11.Player " +
            "join pro1_11.Position p on Player.Position = p.ID "
            + "join pro1_11.Team T on Player.Team = t.ID ";
        
        _players = new AvaloniaList<Player>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read() && reader.HasRows)
        {

            var curPlayer = new Player()
            {
                ID = reader.GetInt32("ID"),
                Name = reader.GetString("Name"),
                Position = reader.GetString("Position"),
                Weight = reader.GetInt32("Weight"),
                Height = reader.GetInt32("Height"),
                DateOfBirth = reader.GetDateTime("DateOfBirth"),
                DateOfStartMatch = reader.GetDateTime("DateOfStartMatch"),
                Team = reader.GetString("Team")
            };
            _players.Add(curPlayer);
        }

        _connection.Close();

        PlayerDataGrid.ItemsSource = _players;

    }
    
    private void AddPlayernBTN_OnClick(object? sender, RoutedEventArgs e)
    {
        AddPlayerWindow addPlayer = new AddPlayerWindow();
        addPlayer.ShowDialog(this);
    }
    
    private void EditPlayerBTN_OnClick(object? sender, RoutedEventArgs e)
    {
        var MyValue = PlayerDataGrid.SelectedItem as Player;
        if (MyValue is null) return;

        EditPlayerWindow edit = new EditPlayerWindow(MyValue);
        edit.ShowDialog(this);
        edit.Closed += (o, args) =>
        {
            if (edit.Result)
            {
                ShowPlayerTable();
            }
        };

    }
    private void DeletePlayerBTN_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = PlayerDataGrid.SelectedItem as Player;
        if (myValue is null)
        {
            return;
        }

        DeletePlayerWindow delete = new DeletePlayerWindow(myValue);
        delete.ShowDialog(this);
        delete.Closed += (o, args) =>
        {
            if (delete.Result)
            {
                ShowPlayerTable();
            }
        };
    }
    public AvaloniaList<Player> PlayerPreSearch
    {
        get { return _playerPreSearch; }
        set { _playerPreSearch = value; }
    }

    public AvaloniaList<Player> Player
    {
        get { return _players; }
        set { _players = value; }
    }

    private void SearchNameTB_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_playerPreSearch is null)
        {
            PlayerPreSearch = Player;
        }

        if (searchNameTB.Text is null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(searchNameTB.Text))
        {
            PlayerDataGrid.ItemsSource = PlayerPreSearch;
            searchTextBlock.IsVisible = false;
            return;
        }

        FilterForPlayer();
    }

    private void FilterForPlayer()
    {
        if (searchNameTB.Text is null)
        {
            return;
        }
        else
        {
            if (sortPositionCB.SelectedIndex == 0)
            {
                var filtered = PlayerPreSearch.Where(
                    it => it.Name.Contains(searchTB.Text)).ToList();
                filtered = filtered.OrderBy(name => name.Name).ToList();
                PlayerDataGrid.ItemsSource = filtered;
            }
            if (sortPositionCB.SelectedIndex == 1)
            {
                var filtered = PlayerPreSearch.Where(
                    it => it.Position.Contains(searchTB.Text)).ToList();
                filtered = filtered.OrderBy(position => position.Position).ToList();
                PlayerDataGrid.ItemsSource = filtered;
            }

        }
    }
    private void SortPositionCB_OnSelectionChanged(object? sender, SelectionChangedEventArgs e) => FilterForPlayer();
}
