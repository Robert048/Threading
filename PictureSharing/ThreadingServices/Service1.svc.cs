using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ThreadingServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
            public string GetGebruikersNaam(long gebrID)
            {
                using (ThreadingEntities ent = new ThreadingEntities())
                {
                    return (from g in ent.Users where g.GebruikerID == gebrID select g.GebruikerNaam).First();
                }
            }

            public string GetGebruikersPW(long gebrID)
            {
                using (ThreadingEntities ent = new ThreadingEntities())
                {
                    return (from g in ent.Users where g.GebruikerID == gebrID select g.GebruikersPW).First();
                }
            }

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

            public void AddGebruiker(User gebruiker)
            {
                using (ThreadingEntities ent = new ThreadingEntities())
                {
                    ent.Users.Attach(gebruiker);
                    ent.Users.Add(gebruiker);
                    ent.SaveChanges();
                }
            }

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

            public void UploadFoto()
            {
                using (ThreadingEntities ent = new ThreadingEntities())
                {

                }
            }

            public string GetFotoNaam(long fotoID)
            {
                using (ThreadingEntities ent = new ThreadingEntities())
                {
                    return (from f in ent.Fotoes where f.FotoID == fotoID select f.FotoNaam).First();
                }
            }

            public string GetFotoPath(long fotoId)
            {
                using (ThreadingEntities ent = new ThreadingEntities())
                {
                    return (from f in ent.Fotoes where f.FotoID == fotoId select f.Path).First();
                }
            }

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

            public void AddFoto(Foto foto)
            {
                using (ThreadingEntities ent = new ThreadingEntities())
                {
                    ent.Fotoes.Add(foto);
                }
            }

            public long GetGebruikerID(long fotoID)
            {
                using (ThreadingEntities ent = new ThreadingEntities())
                {
                    return (from f in ent.Fotoes where f.FotoID == fotoID select f.GebruikerID).First();
                }
            }

            public Foto GetFotoByGebruiker(long gebrID)
            {
                using (ThreadingEntities ent = new ThreadingEntities())
                {
                    return (from f in ent.Fotoes where f.GebruikerID == gebrID select f).First();
                }
            }

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

            public void DeleteFoto(long fotoID)
            {
                using (ThreadingEntities ent = new ThreadingEntities())
                {
                    var removeFoto = (from f in ent.Fotoes where f.FotoID == fotoID select f).First();
                    ent.Fotoes.Remove(removeFoto);
                }
            }
        }
    }
