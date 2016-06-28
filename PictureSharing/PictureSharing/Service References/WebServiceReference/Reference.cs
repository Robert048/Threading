﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 14.0.23107.0
// 
namespace PictureSharing.WebServiceReference {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/ThreadingServices")]
    public partial class User : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string PasswordField;
        
        private long UserIdField;
        
        private string UsernameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Photo", Namespace="http://schemas.datacontract.org/2004/07/ThreadingServices")]
    public partial class Photo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private byte[] ImageDataField;
        
        private string PathField;
        
        private long PhotoIdField;
        
        private string PhotoNameField;
        
        private long UserIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] ImageData {
            get {
                return this.ImageDataField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageDataField, value) != true)) {
                    this.ImageDataField = value;
                    this.RaisePropertyChanged("ImageData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Path {
            get {
                return this.PathField;
            }
            set {
                if ((object.ReferenceEquals(this.PathField, value) != true)) {
                    this.PathField = value;
                    this.RaisePropertyChanged("Path");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long PhotoId {
            get {
                return this.PhotoIdField;
            }
            set {
                if ((this.PhotoIdField.Equals(value) != true)) {
                    this.PhotoIdField = value;
                    this.RaisePropertyChanged("PhotoId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhotoName {
            get {
                return this.PhotoNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PhotoNameField, value) != true)) {
                    this.PhotoNameField = value;
                    this.RaisePropertyChanged("PhotoName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WebServiceReference.IThreadingWebService")]
    public interface IThreadingWebService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/GetUsername", ReplyAction="http://tempuri.org/IThreadingWebService/GetUsernameResponse")]
        System.Threading.Tasks.Task<string> GetUsernameAsync(long userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/GetUserPassword", ReplyAction="http://tempuri.org/IThreadingWebService/GetUserPasswordResponse")]
        System.Threading.Tasks.Task<string> GetUserPasswordAsync(long userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/SetUsername", ReplyAction="http://tempuri.org/IThreadingWebService/SetUsernameResponse")]
        System.Threading.Tasks.Task<bool> SetUsernameAsync(string username, long userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/SetUserPassword", ReplyAction="http://tempuri.org/IThreadingWebService/SetUserPasswordResponse")]
        System.Threading.Tasks.Task<bool> SetUserPasswordAsync(string password, long userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/AddUser", ReplyAction="http://tempuri.org/IThreadingWebService/AddUserResponse")]
        System.Threading.Tasks.Task AddUserAsync(PictureSharing.WebServiceReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/GetUser", ReplyAction="http://tempuri.org/IThreadingWebService/GetUserResponse")]
        System.Threading.Tasks.Task<PictureSharing.WebServiceReference.User> GetUserAsync(long userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/GetUserByName", ReplyAction="http://tempuri.org/IThreadingWebService/GetUserByNameResponse")]
        System.Threading.Tasks.Task<bool> GetUserByNameAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/GetAllUsers", ReplyAction="http://tempuri.org/IThreadingWebService/GetAllUsersResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<PictureSharing.WebServiceReference.User>> GetAllUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/EnterCredentials", ReplyAction="http://tempuri.org/IThreadingWebService/EnterCredentialsResponse")]
        System.Threading.Tasks.Task<PictureSharing.WebServiceReference.User> EnterCredentialsAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/UploadPhoto", ReplyAction="http://tempuri.org/IThreadingWebService/UploadPhotoResponse")]
        System.Threading.Tasks.Task<string> UploadPhotoAsync(string filename, byte[] imageStream, long userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/GetPhotoName", ReplyAction="http://tempuri.org/IThreadingWebService/GetPhotoNameResponse")]
        System.Threading.Tasks.Task<string> GetPhotoNameAsync(long photoId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/GetPhotoPath", ReplyAction="http://tempuri.org/IThreadingWebService/GetPhotoPathResponse")]
        System.Threading.Tasks.Task<string> GetPhotoPathAsync(long photoId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/GetAllPhotos", ReplyAction="http://tempuri.org/IThreadingWebService/GetAllPhotosResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<PictureSharing.WebServiceReference.Photo>> GetAllPhotosAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/SetPhotoPath", ReplyAction="http://tempuri.org/IThreadingWebService/SetPhotoPathResponse")]
        System.Threading.Tasks.Task<bool> SetPhotoPathAsync(long photoId, string newPhotoPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/SetPhotoName", ReplyAction="http://tempuri.org/IThreadingWebService/SetPhotoNameResponse")]
        System.Threading.Tasks.Task<bool> SetPhotoNameAsync(long photoId, string newPhotoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/AddPhoto", ReplyAction="http://tempuri.org/IThreadingWebService/AddPhotoResponse")]
        System.Threading.Tasks.Task AddPhotoAsync(PictureSharing.WebServiceReference.Photo photo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/GetUserByPhoto", ReplyAction="http://tempuri.org/IThreadingWebService/GetUserByPhotoResponse")]
        System.Threading.Tasks.Task<long> GetUserByPhotoAsync(long photoId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/GetPhotoByUser", ReplyAction="http://tempuri.org/IThreadingWebService/GetPhotoByUserResponse")]
        System.Threading.Tasks.Task<PictureSharing.WebServiceReference.Photo> GetPhotoByUserAsync(long userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/DeletePhoto", ReplyAction="http://tempuri.org/IThreadingWebService/DeletePhotoResponse")]
        System.Threading.Tasks.Task DeletePhotoAsync(long photoId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IThreadingWebService/GetAllPhotosFromUser", ReplyAction="http://tempuri.org/IThreadingWebService/GetAllPhotosFromUserResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<PictureSharing.WebServiceReference.Photo>> GetAllPhotosFromUserAsync(long userId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IThreadingWebServiceChannel : PictureSharing.WebServiceReference.IThreadingWebService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ThreadingWebServiceClient : System.ServiceModel.ClientBase<PictureSharing.WebServiceReference.IThreadingWebService>, PictureSharing.WebServiceReference.IThreadingWebService {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ThreadingWebServiceClient() : 
                base(ThreadingWebServiceClient.GetDefaultBinding(), ThreadingWebServiceClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IThreadingWebService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ThreadingWebServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(ThreadingWebServiceClient.GetBindingForEndpoint(endpointConfiguration), ThreadingWebServiceClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ThreadingWebServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ThreadingWebServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ThreadingWebServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ThreadingWebServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ThreadingWebServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<string> GetUsernameAsync(long userId) {
            return base.Channel.GetUsernameAsync(userId);
        }
        
        public System.Threading.Tasks.Task<string> GetUserPasswordAsync(long userId) {
            return base.Channel.GetUserPasswordAsync(userId);
        }
        
        public System.Threading.Tasks.Task<bool> SetUsernameAsync(string username, long userId) {
            return base.Channel.SetUsernameAsync(username, userId);
        }
        
        public System.Threading.Tasks.Task<bool> SetUserPasswordAsync(string password, long userId) {
            return base.Channel.SetUserPasswordAsync(password, userId);
        }
        
        public System.Threading.Tasks.Task AddUserAsync(PictureSharing.WebServiceReference.User user) {
            return base.Channel.AddUserAsync(user);
        }
        
        public System.Threading.Tasks.Task<PictureSharing.WebServiceReference.User> GetUserAsync(long userId) {
            return base.Channel.GetUserAsync(userId);
        }
        
        public System.Threading.Tasks.Task<bool> GetUserByNameAsync(string username) {
            return base.Channel.GetUserByNameAsync(username);
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<PictureSharing.WebServiceReference.User>> GetAllUsersAsync() {
            return base.Channel.GetAllUsersAsync();
        }
        
        public System.Threading.Tasks.Task<PictureSharing.WebServiceReference.User> EnterCredentialsAsync(string username, string password) {
            return base.Channel.EnterCredentialsAsync(username, password);
        }
        
        public System.Threading.Tasks.Task<string> UploadPhotoAsync(string filename, byte[] imageStream, long userId) {
            return base.Channel.UploadPhotoAsync(filename, imageStream, userId);
        }
        
        public System.Threading.Tasks.Task<string> GetPhotoNameAsync(long photoId) {
            return base.Channel.GetPhotoNameAsync(photoId);
        }
        
        public System.Threading.Tasks.Task<string> GetPhotoPathAsync(long photoId) {
            return base.Channel.GetPhotoPathAsync(photoId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<PictureSharing.WebServiceReference.Photo>> GetAllPhotosAsync() {
            return base.Channel.GetAllPhotosAsync();
        }
        
        public System.Threading.Tasks.Task<bool> SetPhotoPathAsync(long photoId, string newPhotoPath) {
            return base.Channel.SetPhotoPathAsync(photoId, newPhotoPath);
        }
        
        public System.Threading.Tasks.Task<bool> SetPhotoNameAsync(long photoId, string newPhotoName) {
            return base.Channel.SetPhotoNameAsync(photoId, newPhotoName);
        }
        
        public System.Threading.Tasks.Task AddPhotoAsync(PictureSharing.WebServiceReference.Photo photo) {
            return base.Channel.AddPhotoAsync(photo);
        }
        
        public System.Threading.Tasks.Task<long> GetUserByPhotoAsync(long photoId) {
            return base.Channel.GetUserByPhotoAsync(photoId);
        }
        
        public System.Threading.Tasks.Task<PictureSharing.WebServiceReference.Photo> GetPhotoByUserAsync(long userId) {
            return base.Channel.GetPhotoByUserAsync(userId);
        }
        
        public System.Threading.Tasks.Task DeletePhotoAsync(long photoId) {
            return base.Channel.DeletePhotoAsync(photoId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<PictureSharing.WebServiceReference.Photo>> GetAllPhotosFromUserAsync(long userId) {
            return base.Channel.GetAllPhotosFromUserAsync(userId);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IThreadingWebService)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IThreadingWebService)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:39046/ThreadingWebService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return ThreadingWebServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IThreadingWebService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return ThreadingWebServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IThreadingWebService);
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_IThreadingWebService,
        }
    }
}
