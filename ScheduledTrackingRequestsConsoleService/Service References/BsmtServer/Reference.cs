﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScheduledTrackingRequestsConsoleService.BsmtServer {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TextToSend", Namespace="http://uGitFit.com/")]
    [System.SerializableAttribute()]
    public partial class TextToSend : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Guid PersonIdField;
        
        private System.DateTime CurrentLocalTimeField;
        
        private System.Guid UserScheduleIdField;
        
        private int LangaugeIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReferenceTextField;
        
        private System.DateTime LocalTimeToSendField;
        
        private bool IsDebugNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TranslatedTextField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Guid PersonId {
            get {
                return this.PersonIdField;
            }
            set {
                if ((this.PersonIdField.Equals(value) != true)) {
                    this.PersonIdField = value;
                    this.RaisePropertyChanged("PersonId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public System.DateTime CurrentLocalTime {
            get {
                return this.CurrentLocalTimeField;
            }
            set {
                if ((this.CurrentLocalTimeField.Equals(value) != true)) {
                    this.CurrentLocalTimeField = value;
                    this.RaisePropertyChanged("CurrentLocalTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public System.Guid UserScheduleId {
            get {
                return this.UserScheduleIdField;
            }
            set {
                if ((this.UserScheduleIdField.Equals(value) != true)) {
                    this.UserScheduleIdField = value;
                    this.RaisePropertyChanged("UserScheduleId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public int LangaugeId {
            get {
                return this.LangaugeIdField;
            }
            set {
                if ((this.LangaugeIdField.Equals(value) != true)) {
                    this.LangaugeIdField = value;
                    this.RaisePropertyChanged("LangaugeId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string ReferenceText {
            get {
                return this.ReferenceTextField;
            }
            set {
                if ((object.ReferenceEquals(this.ReferenceTextField, value) != true)) {
                    this.ReferenceTextField = value;
                    this.RaisePropertyChanged("ReferenceText");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=5)]
        public System.DateTime LocalTimeToSend {
            get {
                return this.LocalTimeToSendField;
            }
            set {
                if ((this.LocalTimeToSendField.Equals(value) != true)) {
                    this.LocalTimeToSendField = value;
                    this.RaisePropertyChanged("LocalTimeToSend");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=6)]
        public bool IsDebugNumber {
            get {
                return this.IsDebugNumberField;
            }
            set {
                if ((this.IsDebugNumberField.Equals(value) != true)) {
                    this.IsDebugNumberField = value;
                    this.RaisePropertyChanged("IsDebugNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string PhoneNumber {
            get {
                return this.PhoneNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneNumberField, value) != true)) {
                    this.PhoneNumberField = value;
                    this.RaisePropertyChanged("PhoneNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=10)]
        public string TranslatedText {
            get {
                return this.TranslatedTextField;
            }
            set {
                if ((object.ReferenceEquals(this.TranslatedTextField, value) != true)) {
                    this.TranslatedTextField = value;
                    this.RaisePropertyChanged("TranslatedText");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://uGitFit.com/", ConfigurationName="BsmtServer.ugfServiceSoap")]
    public interface ugfServiceSoap {
        
        // CODEGEN: Generating message contract since element name userName from namespace http://uGitFit.com/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://uGitFit.com/RetrieveTextToSend", ReplyAction="*")]
        ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendResponse RetrieveTextToSend(ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendRequest request);
        
        // CODEGEN: Generating message contract since element name userName from namespace http://uGitFit.com/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://uGitFit.com/CreateTrackingRequest", ReplyAction="*")]
        ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestResponse CreateTrackingRequest(ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RetrieveTextToSendRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RetrieveTextToSend", Namespace="http://uGitFit.com/", Order=0)]
        public ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendRequestBody Body;
        
        public RetrieveTextToSendRequest() {
        }
        
        public RetrieveTextToSendRequest(ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://uGitFit.com/")]
    public partial class RetrieveTextToSendRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string pwd;
        
        public RetrieveTextToSendRequestBody() {
        }
        
        public RetrieveTextToSendRequestBody(string userName, string pwd) {
            this.userName = userName;
            this.pwd = pwd;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RetrieveTextToSendResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RetrieveTextToSendResponse", Namespace="http://uGitFit.com/", Order=0)]
        public ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendResponseBody Body;
        
        public RetrieveTextToSendResponse() {
        }
        
        public RetrieveTextToSendResponse(ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://uGitFit.com/")]
    public partial class RetrieveTextToSendResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ScheduledTrackingRequestsConsoleService.BsmtServer.TextToSend[] RetrieveTextToSendResult;
        
        public RetrieveTextToSendResponseBody() {
        }
        
        public RetrieveTextToSendResponseBody(ScheduledTrackingRequestsConsoleService.BsmtServer.TextToSend[] RetrieveTextToSendResult) {
            this.RetrieveTextToSendResult = RetrieveTextToSendResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreateTrackingRequestRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CreateTrackingRequest", Namespace="http://uGitFit.com/", Order=0)]
        public ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestRequestBody Body;
        
        public CreateTrackingRequestRequest() {
        }
        
        public CreateTrackingRequestRequest(ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://uGitFit.com/")]
    public partial class CreateTrackingRequestRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string pwd;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public System.DateTime clientLocalTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public System.Guid personId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string textSent;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public System.Guid userScheduleId;
        
        public CreateTrackingRequestRequestBody() {
        }
        
        public CreateTrackingRequestRequestBody(string userName, string pwd, System.DateTime clientLocalTime, System.Guid personId, string textSent, System.Guid userScheduleId) {
            this.userName = userName;
            this.pwd = pwd;
            this.clientLocalTime = clientLocalTime;
            this.personId = personId;
            this.textSent = textSent;
            this.userScheduleId = userScheduleId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreateTrackingRequestResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CreateTrackingRequestResponse", Namespace="http://uGitFit.com/", Order=0)]
        public ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestResponseBody Body;
        
        public CreateTrackingRequestResponse() {
        }
        
        public CreateTrackingRequestResponse(ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://uGitFit.com/")]
    public partial class CreateTrackingRequestResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CreateTrackingRequestResult;
        
        public CreateTrackingRequestResponseBody() {
        }
        
        public CreateTrackingRequestResponseBody(string CreateTrackingRequestResult) {
            this.CreateTrackingRequestResult = CreateTrackingRequestResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ugfServiceSoapChannel : ScheduledTrackingRequestsConsoleService.BsmtServer.ugfServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ugfServiceSoapClient : System.ServiceModel.ClientBase<ScheduledTrackingRequestsConsoleService.BsmtServer.ugfServiceSoap>, ScheduledTrackingRequestsConsoleService.BsmtServer.ugfServiceSoap {
        
        public ugfServiceSoapClient() {
        }
        
        public ugfServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ugfServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ugfServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ugfServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendResponse ScheduledTrackingRequestsConsoleService.BsmtServer.ugfServiceSoap.RetrieveTextToSend(ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendRequest request) {
            return base.Channel.RetrieveTextToSend(request);
        }
        
        public ScheduledTrackingRequestsConsoleService.BsmtServer.TextToSend[] RetrieveTextToSend(string userName, string pwd) {
            ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendRequest inValue = new ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendRequest();
            inValue.Body = new ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendRequestBody();
            inValue.Body.userName = userName;
            inValue.Body.pwd = pwd;
            ScheduledTrackingRequestsConsoleService.BsmtServer.RetrieveTextToSendResponse retVal = ((ScheduledTrackingRequestsConsoleService.BsmtServer.ugfServiceSoap)(this)).RetrieveTextToSend(inValue);
            return retVal.Body.RetrieveTextToSendResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestResponse ScheduledTrackingRequestsConsoleService.BsmtServer.ugfServiceSoap.CreateTrackingRequest(ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestRequest request) {
            return base.Channel.CreateTrackingRequest(request);
        }
        
        public string CreateTrackingRequest(string userName, string pwd, System.DateTime clientLocalTime, System.Guid personId, string textSent, System.Guid userScheduleId) {
            ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestRequest inValue = new ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestRequest();
            inValue.Body = new ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestRequestBody();
            inValue.Body.userName = userName;
            inValue.Body.pwd = pwd;
            inValue.Body.clientLocalTime = clientLocalTime;
            inValue.Body.personId = personId;
            inValue.Body.textSent = textSent;
            inValue.Body.userScheduleId = userScheduleId;
            ScheduledTrackingRequestsConsoleService.BsmtServer.CreateTrackingRequestResponse retVal = ((ScheduledTrackingRequestsConsoleService.BsmtServer.ugfServiceSoap)(this)).CreateTrackingRequest(inValue);
            return retVal.Body.CreateTrackingRequestResult;
        }
    }
}
