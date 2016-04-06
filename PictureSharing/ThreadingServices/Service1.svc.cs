using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;
using System.Web.Script;

namespace ThreadingServices
{
	public class Service1 : IService1
	{
		// Haal de gebruikersnaam op uit de database
		public string GetGebruikersNaam(long gebrID)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from g in ent.Users where g.GebruikerID == gebrID select g.GebruikerNaam).First();
			}
		}

		// Haal het wachtwoord op uit de database
		public string GetGebruikersPW(long gebrID)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from g in ent.Users where g.GebruikerID == gebrID select g.GebruikersPW).First();
			}
		}

		// Haal de gebruiker uit de database aan de hand van de gebruikers ID en return een USER object met de informatie
		public User GetGebruiker(long gebrID)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				User gebr = new User();
				var dbGebr = (from g in ent.Users where g.GebruikerID == gebrID select g).First();
				if (dbGebr != null)
				{
					gebr.GebruikerID = dbGebr.GebruikerID;
					gebr.GebruikerNaam = dbGebr.GebruikerNaam;
					gebr.GebruikersPW = dbGebr.GebruikersPW;
					return gebr;
				}
				return null;
			}
		}

		// Methode die boolean terug geeft wanneer gebruiker wel of niet bestaat
		public bool GetGebruikerByName(String gebruikersnaam)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var gebruiker = (from g in ent.Users where g.GebruikerNaam == gebruikersnaam select g).First();
				if (gebruiker != null)
				{
					return true;
				}
				return false;
			}
		}

		// Methode die een USER object returned aan de hand van de gebruikersnaam en wachtwoord van de gebruiker
		public User InlogMethode(String gebrNaam, String password)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				User gebruiker = new User();

				// Return a user where password and gebr name are the same in the db as given. Else return null
				var user =
						(from g in ent.Users where g.GebruikerNaam == gebrNaam && g.GebruikersPW == password select g)
								.First();
				if (user != null)
				{
					gebruiker.GebruikerID = user.GebruikerID;
					gebruiker.GebruikerNaam = user.GebruikerNaam;
					gebruiker.GebruikersPW = user.GebruikersPW;

					return gebruiker;
				}
				return null;
			}
		}

		// Verander de gebruikersnaam in de database aan de hand van gebruikersID
		public bool SetGebruikersNaam(String gebrNaam, long gebrID)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var user = (from g in ent.Users where g.GebruikerID == gebrID select g).First();
				if (user != null)
				{
					user.GebruikerNaam = gebrNaam;
					ent.SaveChanges();
					return true;
				}
				return false;
			}
		}

		// Set het wachtwoord van de gebruiker in de database aan de hand van de gebruikersID
		public bool SetGebruikersPW(String pw, long gebrID)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var user = (from g in ent.Users where g.GebruikerID == gebrID select g).First();
				if (user != null)
				{
					user.GebruikersPW = pw;
					ent.SaveChanges();
					return true;
				}
				return false;
			}
		}

		// Voeg een nieuwe gebruiker toe aan de database
		public void AddGebruiker(User gebruiker)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				ent.Users.Attach(gebruiker);
				ent.Users.Add(gebruiker);
				ent.SaveChanges();
			}
		}

		// Haal alle aanwezige gebruikers op uit de database
		public List<User> GetAllGebruikers()
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var userList = (from g in ent.Users select g);
				List<User> returnList = new List<User>();

				foreach (var user in userList)
				{
					User tempU = new User();

					tempU.GebruikerID = user.GebruikerID;
					tempU.GebruikerNaam = user.GebruikerNaam;
					tempU.GebruikersPW = user.GebruikersPW;

					returnList.Add(tempU);
				}
				return returnList;
			}
		}

		// Upload een foto en store hem lokaal, zet een referentie naar de foto in de database
		public string UploadFoto(String filename, byte[] imageStream, long gebrID)
		{

			string filePath = Path.Combine((Environment.GetFolderPath(Environment.SpecialFolder.Desktop)), filename); // Host HostingEnvironment.MapPath
			int length = 0;
			Stream stream = new MemoryStream(imageStream);

			stream.Position = 0;

			using (FileStream writer = new FileStream(filePath, FileMode.Create))
			{
				int readCount;
				var buffer = new byte[8192];
				while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
				{
					writer.Write(buffer, 0, readCount);
					length += readCount;
				}
			}
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				Foto foto = new Foto();
				foto.FotoNaam = filename;
				foto.Path = filePath;
				foto.GebruikerID = gebrID;

				ent.Fotoes.Add(foto);
				ent.Fotoes.Attach(foto);
				ent.SaveChanges();
			}

			return "Succes";
		}

		// Haal de naam van de foto op uit de database aan de hand van fotoID
		public string GetFotoNaam(long fotoID)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from f in ent.Fotoes where f.FotoID == fotoID select f.FotoNaam).First();
			}
		}

		// Haal het pad op van de opgeslagen foto uit de database aan de hand van fotoID
		public string GetFotoPath(long fotoId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from f in ent.Fotoes where f.FotoID == fotoId select f.Path).First();
			}
		}

		// Zet een nieuw pad voor een foto aan de hand van fotoID
		public bool SetFotoPath(long fotoID, string path)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var foto = (from f in ent.Fotoes where f.FotoID == fotoID select f).First();
				if (foto != null)
				{
					foto.Path = path;
					ent.SaveChanges();
					return true;
				}
				return false;
			}
		}

		// Geef de foto een nieuwe naam aan de hand van fotoID
		public bool SetFotoNaam(long fotoID, string naam)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var foto = (from f in ent.Fotoes where f.FotoID == fotoID select f).First();
				if (foto != null)
				{
					foto.FotoNaam = naam;
					ent.SaveChanges();
					return true;
				}
				return true;
			}
		}

		// Voeg een nieuwe foto toe aan de database
		public void AddFoto(Foto foto)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				ent.Fotoes.Add(foto);
			}
		}

		// Haal het gebruikersID op van de foto aan de hand van fotoID
		public long GetGebruikerID(long fotoID)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from f in ent.Fotoes where f.FotoID == fotoID select f.GebruikerID).First();
			}
		}

		// Return een foto object aan de hand van gebruikerID
		public Foto GetFotoByGebruiker(long gebrID)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from f in ent.Fotoes where f.GebruikerID == gebrID select f).First();
			}
		}

		// Return een lijst met foto's uit de database
		public List<Foto> GetAllFotos()
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var fotoList = (from f in ent.Fotoes select f);
				List<Foto> returnList = new List<Foto>();
				foreach (var foto in fotoList)
				{
					Foto tempFoto = new Foto();
					tempFoto.FotoID = foto.FotoID;
					tempFoto.FotoNaam = foto.FotoNaam;
					tempFoto.GebruikerID = foto.GebruikerID;
					tempFoto.Path = foto.Path;
					returnList.Add(tempFoto);
				}
				return returnList;
			}
		}

		// Return een lijst met foto's aan de hand van gebruikerID
		public List<Foto> GetAllFotosById(long gebrId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var fotoList = (from f in ent.Fotoes where f.GebruikerID == gebrId select f);
				List<Foto> returnList = new List<Foto>();
				foreach (var foto in fotoList)
				{
					Foto tempFoto = new Foto();
					tempFoto.FotoID = foto.FotoID;
					tempFoto.FotoNaam = foto.FotoNaam;
					tempFoto.GebruikerID = foto.GebruikerID;
					tempFoto.Path = foto.Path;
					returnList.Add(tempFoto);
				}
				return returnList;
			}
		}

		// Delete een foto uit de database
		public void DeleteFoto(long fotoID)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var removeFoto = (from f in ent.Fotoes where f.FotoID == fotoID select f).First();
				ent.Fotoes.Remove(removeFoto);
				ent.SaveChangesAsync();
			}
		}
	}
}
