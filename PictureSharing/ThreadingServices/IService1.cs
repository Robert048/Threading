using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ThreadingServices
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetGebruikersNaam(long gebrID);

        [OperationContract]
        string GetGebruikersPW(long gebrID);

        [OperationContract]
        bool SetGebruikersNaam(String gebruikersNaam, long gebrID);

        [OperationContract]
        bool SetGebruikersPW(String password, long gebrID);

        [OperationContract]
        void AddGebruiker(User gebruiker);

        [OperationContract]
        User GetGebruiker(long gebrID);

        [OperationContract]
        List<User> GetAllGebruikers();

        [OperationContract]
        User InlogMethode(String gebrNaam, String password);

        // TODO: Uitzoeken en uitbreiden
        [OperationContract]
        void UploadFoto(string filename, Stream stream);
        // ---------------------------

        [OperationContract]
        string GetFotoNaam(long fotoID);

        [OperationContract]
        string GetFotoPath(long fotoID);

        [OperationContract]
        List<Foto> GetAllFotos();

        [OperationContract]
        bool SetFotoPath(long fotoID, string fotoPath);

        [OperationContract]
        bool SetFotoNaam(long fotoID, string fotoNaam);

        [OperationContract]
        void AddFoto(Foto foto);

        [OperationContract]
        long GetGebruikerID(long fotoID);

        [OperationContract]
        Foto GetFotoByGebruiker(long gebrID);

        [OperationContract]
        void DeleteFoto(long fotoID);

        [OperationContract]
        List<Foto> GetAllFotosById(long gebrId);
    }
}
