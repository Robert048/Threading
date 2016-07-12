using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThreadingServices
{
	public class ThreadingWebService : IThreadingWebService
	{
		// Haal de gebruikersnaam op uit de database
		public string GetUsername(long userId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from g in ent.Users where g.UserId == userId select g.Username).First();
			}
		}

		// Haal het wachtwoord op uit de database
		public string GetUserPassword(long userId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from g in ent.Users where g.UserId == userId select g.Password).First();
			}
		}

		// Haal de gebruiker uit de database aan de hand van de gebruikers ID en return een USER object met de informatie
		public User GetUser(long userId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				User gebr = new User();
				var dbGebr = (from g in ent.Users where g.UserId == userId select g).First();
				if (dbGebr != null)
				{
					gebr.UserId = dbGebr.UserId;
					gebr.Username = dbGebr.Username;
					gebr.Password = dbGebr.Password;
					return gebr;
				}
				return null;
			}
		}

		// Methode die boolean terug geeft wanneer gebruiker wel of niet bestaat
		public bool GetUserByName(string username)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var gebruiker = (from g in ent.Users where g.Username == username select g).First();
				if (gebruiker != null)
				{
					return true;
				}
				return false;
			}
		}

		// Methode die een USER object returned aan de hand van de gebruikersnaam en wachtwoord van de gebruiker
		public User EnterCredentials(string username, string password)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				User gebruiker = new User();

				// Return a user where password and gebr name are the same in the db as given. Else return null
				var user =
						(from g in ent.Users where g.Username == username && g.Password == password select g)
								.First();
				if (user != null)
				{
					gebruiker.UserId = user.UserId;
					gebruiker.Username = user.Username;
					gebruiker.Password = user.Password;

					return gebruiker;
				}
				return null;
			}
		}

		// Verander de gebruikersnaam in de database aan de hand van gebruikersID
		public bool SetUsername(string gebrNaam, long userId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var user = (from g in ent.Users where g.UserId == userId select g).First();
				if (user != null)
				{
					user.Username = gebrNaam;
					ent.SaveChanges();
					return true;
				}
				return false;
			}
		}

		// Set het wachtwoord van de gebruiker in de database aan de hand van de gebruikersID
		public bool SetUserPassword(string pw, long userId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var user = (from g in ent.Users where g.UserId == userId select g).First();
				if (user != null)
				{
					user.Password = pw;
					ent.SaveChanges();
					return true;
				}
				return false;
			}
		}

		// Voeg een nieuwe gebruiker toe aan de database
		public void AddUser(User user)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				ent.Users.Attach(user);
				ent.Users.Add(user);
				ent.SaveChanges();
			}
		}

		// Haal alle aanwezige gebruikers op uit de database
		public List<User> GetAllUsers()
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var userList = (from g in ent.Users select g);
				List<User> returnList = new List<User>();

				foreach (var user in userList)
				{
					User tempU = new User();

					tempU.UserId = user.UserId;
					tempU.Username = user.Username;
					tempU.Password = user.Password;

					returnList.Add(tempU);
				}
				return returnList;
			}
		}

		// Upload een foto en store hem lokaal, zet een referentie naar de foto in de database
		public string UploadPhoto(string filename, byte[] imageStream, long userId)
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
				Photo foto = new Photo();
				foto.PhotoName = filename;
				foto.Path = filePath;
				foto.UserId = userId;
			    foto.ImageData = imageStream;

				ent.Photos.Attach(foto);
				ent.Photos.Add(foto);
				ent.SaveChanges();
			}

			return "Succes";
		}

		// Haal de naam van de foto op uit de database aan de hand van fotoID
		public string GetPhotoName(long photoId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from f in ent.Photos where f.PhotoId == photoId select f.PhotoName).First();
			}
		}

		// Haal het pad op van de opgeslagen foto uit de database aan de hand van fotoID
		public string GetPhotoPath(long fotoId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from f in ent.Photos where f.PhotoId == fotoId select f.Path).First();
			}
		}

		// Zet een nieuw pad voor een foto aan de hand van fotoID
		public bool SetPhotoPath(long photoId, string path)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var foto = (from f in ent.Photos where f.PhotoId == photoId select f).First();
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
		public bool SetPhotoName(long photoId, string naam)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var foto = (from f in ent.Photos where f.PhotoId == photoId select f).First();
				if (foto != null)
				{
					foto.PhotoName = naam;
					ent.SaveChanges();
					return true;
				}
				return true;
			}
		}

		// Voeg een nieuwe foto toe aan de database
		public void AddPhoto(Photo photo)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				ent.Photos.Add(photo);
			}
		}

		// Haal het gebruikersID op van de foto aan de hand van fotoID
		public long GetUserByPhoto(long photoId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from f in ent.Photos where f.PhotoId == photoId select f.UserId).First();
			}
		}

		// Return een foto object aan de hand van gebruikerID
		public Photo GetPhotoByUser(long userId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from f in ent.Photos where f.UserId == userId select f).First();
			}
		}

		// Return een lijst met foto's uit de database
		public List<Photo> GetAllPhotos()
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return ent.Photos.ToList();
			}
		}

		// Return een lijst met foto's aan de hand van gebruikerID
		public List<Photo> GetAllPhotosFromUser(long userId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				return (from f in ent.Photos where f.UserId == userId select f).ToList();
			}
		}

		// Delete een foto uit de database
		public void DeletePhoto(long photoId)
		{
			using (ThreadingEntities ent = new ThreadingEntities())
			{
				var removeFoto = (from f in ent.Photos where f.PhotoId == photoId select f).First();
				ent.Photos.Remove(removeFoto);
				ent.SaveChanges();
			}
		}
	}
}
