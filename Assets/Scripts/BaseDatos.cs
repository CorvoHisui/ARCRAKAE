using System;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.SqliteClient;

public class BaseDatos : MonoBehaviour
{
    Item item;
    public SqliteConnection OpenConexion()
    {
        SqliteConnection conexion = new SqliteConnection("URI=file:" + Application.dataPath + "/Plugins/inventario.db");
        conexion.Open();
        return conexion;
    }
    public void CrearTabla()
    {
        string creacion = "CREATE TABLE IF NOT EXISTS Inventario (ItemType int(1), amount int(3))";
        SqliteCommand cmd = new SqliteCommand(creacion, OpenConexion());
        cmd.ExecuteNonQuery();

        string creacion1 = "CREATE TABLE IF NOT EXISTS Copia (ItemType int(1), amount int(3))";
        SqliteCommand cmd1 = new SqliteCommand(creacion1, OpenConexion());
        cmd1.ExecuteNonQuery();

        string trigger = "CREATE TRIGGER IF NOT EXISTS objetosBorrados BEFORE DELETE ON Inventario FOR EACH ROW begin INSERT INTO Copia VALUES(OLD.ItemType, OLD.amount);end;";
        SqliteCommand cmd2 = new SqliteCommand(trigger, OpenConexion());
        cmd2.ExecuteNonQuery();

        string trigger1 = "CREATE TRIGGER IF NOT EXISTS objetosactualizados BEFORE UPDATE ON Inventario FOR EACH ROW begin INSERT INTO Copia VALUES(OLD.ItemType, OLD.amount);end;";
        SqliteCommand cmd3 = new SqliteCommand(trigger1, OpenConexion());
        cmd3.ExecuteNonQuery();

        string vista = "CREATE VIEW IF NOT EXISTS ObjetosEnInventario AS SELECT COUNT(DISTINCT ItemType) FROM Inventario; ";
        SqliteCommand cmd4 = new SqliteCommand(vista, OpenConexion());
        cmd4.ExecuteNonQuery();

        OpenConexion().Close();
    }
    public void añadirItem(Item item)
    {
        string insert = @"INSERT INTO Inventario (ItemType, amount) VALUES (@ItemType, @amount)";
        SqliteCommand cmd = new SqliteCommand(insert, OpenConexion());

        cmd.Parameters.Add(new SqliteParameter("@ItemType", item.itemType));
        cmd.Parameters.Add(new SqliteParameter("@amount", item.amount));
        cmd.ExecuteNonQuery();
        OpenConexion().Close();
    }
    public void SumarItem(Item item)
    {
        string alter = @"UPDATE Inventario SET amount = amount+1 WHERE ItemType = @ItemType";
        SqliteCommand cmd = new SqliteCommand(alter, OpenConexion());
        cmd.Parameters.Add(new SqliteParameter("@ItemType", item.itemType));

        cmd.ExecuteNonQuery();
        OpenConexion().Close();
    }
    public void RestarItem(Item item)
    {
        string alter = @"UPDATE Inventario SET amount = amount-1 WHERE ItemType = @ItemType";
        SqliteCommand cmd = new SqliteCommand(alter, OpenConexion());
        cmd.Parameters.Add(new SqliteParameter("@ItemType", item.itemType));

        cmd.ExecuteNonQuery();
        OpenConexion().Close();
    }
    public void EliminarObjeto(Item item)
    {
        string consulta = "DELETE FROM Inventario WHERE ItemType = @ItemType";
        SqliteCommand cmd = new SqliteCommand(consulta, OpenConexion());
        cmd.Parameters.Add(new SqliteParameter("@ItemType", item.itemType));
        cmd.ExecuteNonQuery();
    }
    public void ObjetosTotales()
    {
        string vista = "SELECT * FROM ObjetosEnInventario; ";
        SqliteCommand cmd = new SqliteCommand(vista, OpenConexion());
        SqliteDataReader datos = cmd.ExecuteReader();
        while (datos.Read())
        {
            string objetos = Convert.ToString(datos[0]);
            string mensaje = "Numero de Objetos Unicos en Inventario:" + objetos;
            Debug.Log(mensaje);
        }
    }

}
