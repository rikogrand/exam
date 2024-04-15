using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using SuperVolleyball.Entity;

namespace SuperVolleyball;

public partial class DeletePlayerWindow : Window
{
    private readonly Player _player; 
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";

    public DeletePlayerWindow(Player player)
    {
        InitializeComponent();
        _player = player;
    }

    private void No_Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Yes_Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        MySqlConnection _connection;
        string sql = "SET FOREIGN_KEY_CHECKS=0;" + "Delete from Player where ID = @ID LIMIT 1";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@ID", MySqlDbType.Int32);
        command.Parameters["@ID"].Value = _player.ID;
        command.ExecuteNonQuery();
        _connection.Close();
        Close(true);
    }
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }
    public bool Result { get; private set; }
}
