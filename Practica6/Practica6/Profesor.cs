﻿/*
 * Created by SharpDevelop.
 * User: Pillos
 * Date: 09/05/2014
 * Time: 09:50 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using MySql.Data.MySqlClient;

namespace Practica6
{
	/// <summary>
	/// Description of Profesor.
	/// </summary>
	public class Profesor : Conexion
	{
		public string cod, nom;
		
		public Profesor()
		{
		}
		
		public void MostrarDatos(){
			this.abrirConexion();
            MySqlCommand myCommand = new MySqlCommand(this.querySelect(), 
			                                          myConnection);
            MySqlDataReader myReader = myCommand.ExecuteReader();	
	        while (myReader.Read()){
	            string id = myReader["id"].ToString();
	            string codigo = myReader["codigo"].ToString();
	            string nombre = myReader["nombre"].ToString();
	            
	            Console.WriteLine("Id: "      +   id   + 
				                  "\nCódigo: " + codigo +
				                  "\nNombre: " + nombre );
	            Console.WriteLine("----------------");
	       }

            myReader.Close();
			myReader = null;
            myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();
		}
		
		public void Agregar(string codigo, string nombre){
			this.abrirConexion();
			string sql = "INSERT INTO profesor (codigo, nombre) VALUES ('" + codigo + "', '" + nombre + "')";
			this.ejecutarComando(sql);
			this.cerrarConexion();
		}
		
		public void editarRegistro (string id)
		{
			string cod, nom, ans;
	

			this.abrirConexion ();
			MySqlCommand myCommand = new MySqlCommand (this.queryContId (id), myConnection);
			int res = Convert.ToInt16 (myCommand.ExecuteScalar ());
			myCommand = null;
			this.cerrarConexion();

			this.mostrarPorId (id);
			if (res == 1) {
				Console.WriteLine ("Seguro que desea editar el registro?");
				Console.WriteLine ("1. Si");
				Console.WriteLine ("2. No");
				ans = (Console.ReadLine ());

				if (ans == "1") {

				
					Console.WriteLine ("Ingrese el nuevo codigo: ");
					cod = Console.ReadLine ();
					Console.WriteLine ("Ingrese el nuevo nombre: ");
					nom = Console.ReadLine ();
					
					this.abrirConexion();
					string sql = "UPDATE profesor SET codigo='" + cod + 
						"', nombre='" + nom + 
						"' WHERE id='" + id + "' ";
					Console.WriteLine ("SE EDITÓ CON EXITO");
					this.ejecutarComando (sql);
					this.cerrarConexion ();
				}
			}
		}

		private void mostrarPorId (string id1)
		{
			try {
				this.abrirConexion ();
				MySqlCommand myCommand;
				myCommand= new MySqlCommand (this.queryContId (id1), myConnection);
				int res = Convert.ToInt16 (myCommand.ExecuteScalar ());
				myCommand=null;
				myCommand= new MySqlCommand (this.queryId (id1), myConnection);
				MySqlDataReader myReader = myCommand.ExecuteReader ();
				if (res == 1) {
					while(myReader.Read()){
						string id = myReader ["id"].ToString ();
						string nombre = myReader ["codigo"].ToString ();
						string codigo = myReader ["nombre"].ToString ();

						Console.WriteLine("");
						Console.WriteLine ("Id: " + id +
							"\nCódigo: " + nombre +
							"\nNombre: " + codigo 
						);
						Console.WriteLine("");
					};

				} else {
					Console.WriteLine ("No existe el registro");
				}
				myReader.Close ();
				myReader = null;
				myCommand.Dispose ();
				myCommand = null;
				this.cerrarConexion ();

			} catch(Exception ex) {
				Console.WriteLine(ex);
			}
		}

		
		private void borrarRegistro (string id)
		{
			string ans;
			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand (this.queryContId (id), myConnection);
			int res = Convert.ToInt16 (myCommand.ExecuteScalar ());
			myCommand = null;
			this.cerrarConexion();
			this.mostrarPorId(id);

			if (res == 1) {
				Console.WriteLine ("Seguro que desea borrar el registro?");
				Console.WriteLine ("1. Si");
				Console.WriteLine ("2. No");
				ans = (Console.ReadLine ());

				if (ans == "1") {
					this.abrirConexion();
					string sql = "DELETE FROM profesor WHERE id='" + id + "'";
					Console.WriteLine ("SE BORRÓ CON EXITO");
					this.ejecutarComando (sql);
					this.cerrarConexion ();
				}
			} else {
				Console.WriteLine("No existe el registro con ese id");
			}

		}
		
		public void Menu(){
		 int opcion;
		 string respuesta;
		 
			Console.WriteLine("***** Menu *****");
			Console.WriteLine("----------------");
			Console.WriteLine("");
			Console.WriteLine("1.- Mostrar");
			Console.WriteLine("2.- Agregar");
			Console.WriteLine("3.- Editar");
			Console.WriteLine("4.- Eliminar");
			Console.WriteLine("5.- Salir");
			Console.WriteLine("");
			Console.WriteLine("----------------");
			Console.WriteLine("");
			opcion = int .Parse(Console.ReadLine());
			
			switch (opcion){
				case 1: 
					Console.Clear();
					Console.WriteLine("Mostrar Registro");
					Console.WriteLine("----------------");
					Console.WriteLine("");
					MostrarDatos();
					Console.WriteLine("");
					Console.WriteLine("¿Qué desea hacer?");
					Console.WriteLine("1.- Regresar al menu");
					Console.WriteLine("2.- Salir");
					respuesta = Console.ReadLine();
					
					if(respuesta == "1"){
						Console.Clear();
						Menu();
					}
					else{
						Environment.Exit(0);
					}
				break;
				
			    case 2:
				    Console.Clear();
				    Console.WriteLine("Agregar Registro");
				    Console.WriteLine("----------------");
				    Console.WriteLine("");
				    Console.WriteLine("Ingrese el código");
				    cod = Console.ReadLine();
				    Console.WriteLine("Ingrese el nombre");
				    nom = Console.ReadLine();
				    Agregar(cod,nom);
				    Console.WriteLine("");
				    Console.WriteLine("Se guardo con Exito!!!");
				    Console.WriteLine("");
				    Console.WriteLine("¿Qué desea hacer?");
					Console.WriteLine("1.- Regresar al menu");
					Console.WriteLine("2.- Salir");
					respuesta = Console.ReadLine();
					
					if(respuesta == "1"){
						Console.Clear();
						Menu();
					}
					else{
						Environment.Exit(0);
					}
				break;
				
			    case 3:
				    Console.Clear();
				    string Id;
					Console.WriteLine("Editar Registro");
					Console.WriteLine("----------------");
					Console.WriteLine("");
					Console.WriteLine("Ingrese el id del registro");
					Id = Console.ReadLine ();
					editarRegistro(Id);
				    Console.WriteLine("");
				    Console.WriteLine("¿Qué desea hacer?");
					Console.WriteLine("1.- Regresar al menu");
					Console.WriteLine("2.- Salir");
					respuesta = Console.ReadLine();
					
					if(respuesta == "1"){
						Console.Clear();
						Menu();
					}
					else{
						Environment.Exit(0);
					}
				break;
			   
			    case 4:
				    Console.Clear();
					Console.WriteLine("Eliminar Registro");
					Console.WriteLine("----------------");
					Console.WriteLine("");
					Console.WriteLine ("Ingrese el id a borrar");
					this.borrarRegistro (Console.ReadLine ());
					 Console.WriteLine("");
				    Console.WriteLine("¿Qué desea hacer?");
					Console.WriteLine("1.- Regresar al menu");
					Console.WriteLine("2.- Salir");
					respuesta = Console.ReadLine();
					
					if(respuesta == "1"){
						Console.Clear();
						Menu();
					}
					else{
						Environment.Exit(0);
					}
				break;
				
			    case 5:
				    Environment.Exit(0);
				break;
			}
		 
		}
		
		private int ejecutarComando(string sql){
			MySqlCommand myCommand = new MySqlCommand(sql,this.myConnection);
			int afectadas = myCommand.ExecuteNonQuery();
			myCommand.Dispose();
			myCommand = null;
			return afectadas;
		}
		
		private string querySelect(){
			return "SELECT * " +
	           	"FROM profesor";
		}
		
		private string queryContId (string id2){
			return "SELECT Count(*) FROM profesor where (id='" + id2 + "')";
		}
		
	    private string queryId (string id){
			return "SELECT * FROM profesor where id='"+ id +"' ";
		}
		
	}
}
