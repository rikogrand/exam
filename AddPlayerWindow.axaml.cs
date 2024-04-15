using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using SuperVolleyball.Entity;

namespace SuperVolleyball;

public partial class AddPlayerWindow : Window
{
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    private AvaloniaList<Position> _positions;
    
    private AvaloniaList<Team> _teams;
    public AddPlayerWindow()
    {
        FillPosition();
        FillTeam();
        InitializeComponent();
    }
    private void AddPlayernBTN_OnClick(object? sender, RoutedEventArgs e)
    {
        MySqlConnection _connection;

        string sql =
                "insert into Player (Name, Position, Weight, Height, DateOfBirth, DateOfStartMatch, Team)" +
                " values (@Name, @Position, @Weight, @Height, @DateOfBirth, @DateOfStartMatch, @Team ) "
            ;
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Name", MySqlDbType.String);
        command.Parameters["@Name"].Value = namePlayerTB.Text;
        
        command.Parameters.Add("@Position", MySqlDbType.Int32);
        command.Parameters["@Position"].Value = (positionCB.SelectedItem as SuperVolleyball.Entity.Position).ID;

        command.Parameters.Add("@Weight", MySqlDbType.Int32);
        command.Parameters["@Weight"].Value = weightTB.Text;

        command.Parameters.Add("@Height", MySqlDbType.Int32);
        command.Parameters["@Height"].Value = heightTB.Text;

        command.Parameters.Add("@DateOfBirth", MySqlDbType.DateTime);
        command.Parameters["@DateOfBirth"].Value = dateOfBirthDP.SelectedDate;
        
        command.Parameters.Add("@DateOfStartMatch", MySqlDbType.DateTime);
        command.Parameters["@DateOfStartMatch"].Value = dateOfStartMatchDP.SelectedDate;
        
        command.Parameters.Add("@Team", MySqlDbType.Int32);
        command.Parameters["@Team"].Value = (teamCB.SelectedItem as SuperVolleyball.Entity.Team).ID;
        command.ExecuteNonQuery();
        _connection.Close();
        
        PlayerWindow playerWindow = new PlayerWindow();
        playerWindow.Show();
        this.Close();
        Close(true);
    }
    public void Close(bool result)
    {
        Result = result;
        base.Close(result);
    }

    public bool Result { get; private set; }

    public void FillPosition()
    {
        string sql = "select ID, Name from pro1_11.Position";
        _positions = new AvaloniaList<Position>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curPosition = new Position()
            {
                ID = reader.GetInt32("ID"),
                Name = reader.GetString("Name")
            };
            _positions.Add(curPosition);
        }
        _connection.Close();
        var PositionComboBox = this.Find<ComboBox>("positionCB");
        PositionComboBox.ItemsSource = _positions;
      
    }
    
    public void FillTeam()
    {
        string sql = "select ID, Name from pro1_11.Team";
        _teams = new AvaloniaList<Team>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curTeam = new Team()
            {
                ID = reader.GetInt32("ID"),
                Name = reader.GetString("Name")
            };
            _teams.Add(curTeam);
        }
        _connection.Close();
        var TeamComboBox = this.Find<ComboBox>("teamCB");
        TeamComboBox.ItemsSource = _teams;
      
    }
}