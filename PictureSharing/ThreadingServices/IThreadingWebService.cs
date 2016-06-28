using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ThreadingServices
{
    [ServiceContract]
    public interface IThreadingWebService
    {
        [OperationContract]
        string GetUsername(long userId);

        [OperationContract]
        string GetUserPassword(long userId);

        [OperationContract]
        bool SetUsername(string username, long userId);

        [OperationContract]
        bool SetUserPassword(string password, long userId);

        [OperationContract]
        void AddUser(User user);

        [OperationContract]
        User GetUser(long userId);

		[OperationContract]
		bool GetUserByName(string username);

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        User EnterCredentials(string username, string password);

        [OperationContract]
        string UploadPhoto(string filename, byte[] imageStream, long userId);

        [OperationContract]
        string GetPhotoName(long photoId);

        [OperationContract]
        string GetPhotoPath(long photoId);

        [OperationContract]
        List<Photo> GetAllPhotos();

        [OperationContract]
        bool SetPhotoPath(long photoId, string newPhotoPath);

        [OperationContract]
        bool SetPhotoName(long photoId, string newPhotoName);

        [OperationContract]
        void AddPhoto(Photo photo);

        [OperationContract]
        long GetUserByPhoto(long photoId);

        [OperationContract]
        Photo GetPhotoByUser(long userId);

        [OperationContract]
        void DeletePhoto(long photoId);

        [OperationContract]
        List<Photo> GetAllPhotosFromUser(long userId);
    }
}
