/*
	This file is part of Thunder Aerospace Corporation's (TAC) Fuel Balancer /L Unleashed
		© 2019-2021 Lisias T : http://lisias.net <support@lisias.net>
		© 2019 linuxgurugamer
		© 2016-2018 thwebbooth
		© 2013-2015 Taranis Elsu

	TAC Fuel Balancer /L Unleashed is licensed as follows:

		* CC BY-NC-SA 3.0 : http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode

	TAC Fuel Balancer /L Unleashed is distributed in the hope that
	it will be useful, but WITHOUT ANY WARRANTY; without even the implied
	warranty of	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

*/
using System.Reflection;
using UnityEngine;
using KSP.IO;


namespace Tac
{
	/// <summary>
	/// Contains static methods to assist in creating textures.
	/// </summary>
	internal static class TextureHelper {
		/// <summary>
		/// Creates a new Texture2D from an embedded resource.
		/// </summary>
		/// <param name="resource">The location of the resource in the assembly.</param>
		/// <param name="width">The width of the texture.</param>
		/// <param name="height">The height of the texture.</param>
		/// <returns></returns>
		public static Texture2D FromResource( string resource, int width, int height )
		{
			var tex = new Texture2D( width, height, TextureFormat.ARGB32, false );
			var iconStream = Assembly.GetExecutingAssembly( ).GetManifestResourceStream( resource ).ReadToEnd( );
			if( iconStream == null )
				return null;
			tex.LoadImage( iconStream );
			tex.Apply();
			return tex;
		}



		public static Texture2D LoadImage<T>( string filename, int width, int height )
		{
			if( File.Exists<T>( filename ) )
			{
				var bytes = File.ReadAllBytes<T>( filename );
				Texture2D texture = new Texture2D( width, height, TextureFormat.ARGB32, false );
				texture.LoadImage( bytes );
				return texture;
			}
			else
				return null;
		}

	}
}
