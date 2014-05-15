/*
 * Created by SharpDevelop.
 * User: Pillos
 * Date: 09/05/2014
 * Time: 09:42 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Practica6
{
	class Program : Profesor
	{
		public static void Main(string[] args)
		{
			Profesor p = new Profesor();
			p.Menu();
			Console.ReadKey(true);
		}
	}
}