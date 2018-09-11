using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;

namespace WebApi
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class CommunicationSvcConfig
    {

        private CommunicationSvcConfigXmlSerializerSection xmlSerializerSectionField;

        /// <remarks/>
        public CommunicationSvcConfigXmlSerializerSection xmlSerializerSection
        {
            get
            {
                return this.xmlSerializerSectionField;
            }
            set
            {
                this.xmlSerializerSectionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CommunicationSvcConfigXmlSerializerSection
    {

        private CommunicationSvcConfigXmlSerializerSectionCommSvcConfig commSvcConfigField;

        private string typeField;

        /// <remarks/>
        public CommunicationSvcConfigXmlSerializerSectionCommSvcConfig CommSvcConfig
        {
            get
            {
                return this.commSvcConfigField;
            }
            set
            {
                this.commSvcConfigField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CommunicationSvcConfigXmlSerializerSectionCommSvcConfig
    {

        private object tsPhonebookEntryField;

        private ushort rasStatusTimerIntervalField;

        private bool rasConnectAutoRetryField;

        private byte rasConnectRetryLimitField;

        private ushort rasConnectRetryTimerIntervalField;

        private bool useGsmSimulatorField;

        private ushort smsQueueDelayTimerIntervalField;

        private string clientCertSerialNumberField;

        /// <remarks/>
        public object TsPhonebookEntry
        {
            get
            {
                return this.tsPhonebookEntryField;
            }
            set
            {
                this.tsPhonebookEntryField = value;
            }
        }

        /// <remarks/>
        public ushort RasStatusTimerInterval
        {
            get
            {
                return this.rasStatusTimerIntervalField;
            }
            set
            {
                this.rasStatusTimerIntervalField = value;
            }
        }

        /// <remarks/>
        public bool RasConnectAutoRetry
        {
            get
            {
                return this.rasConnectAutoRetryField;
            }
            set
            {
                this.rasConnectAutoRetryField = value;
            }
        }

        /// <remarks/>
        public byte RasConnectRetryLimit
        {
            get
            {
                return this.rasConnectRetryLimitField;
            }
            set
            {
                this.rasConnectRetryLimitField = value;
            }
        }

        /// <remarks/>
        public ushort RasConnectRetryTimerInterval
        {
            get
            {
                return this.rasConnectRetryTimerIntervalField;
            }
            set
            {
                this.rasConnectRetryTimerIntervalField = value;
            }
        }

        /// <remarks/>
        public bool UseGsmSimulator
        {
            get
            {
                return this.useGsmSimulatorField;
            }
            set
            {
                this.useGsmSimulatorField = value;
            }
        }

        /// <remarks/>
        public ushort SmsQueueDelayTimerInterval
        {
            get
            {
                return this.smsQueueDelayTimerIntervalField;
            }
            set
            {
                this.smsQueueDelayTimerIntervalField = value;
            }
        }

        /// <remarks/>
        public string ClientCertSerialNumber
        {
            get
            {
                return this.clientCertSerialNumberField;
            }
            set
            {
                this.clientCertSerialNumberField = value;
            }
        }
    }

    public class DeviceConfigurationController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public CommunicationSvcConfig Get(int id)
        {
            CommunicationSvcConfig config;
            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><CommunicationSvcConfig><xmlSerializerSection type=\"AbsoluteSystems.CIMA.CashVault.CommunicationService.CommSvcConfig, Cima.Cv.CommunicationService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\"><CommSvcConfig xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><!-- The phonebook entry used to connect to the Transaction Server --><TsPhonebookEntry></TsPhonebookEntry><!--<TsPhonebookEntry>Absolute Systems VPN</TsPhonebookEntry>--><!-- Used to check whether the RAS connection is up --><!-- Unit is milliseconds --><RasStatusTimerInterval>1000</RasStatusTimerInterval><!-- Flag to determine whether a failed RAS connection attempt is automatically retried --><RasConnectAutoRetry>false</RasConnectAutoRetry><!-- Connection attempt retry limit --><RasConnectRetryLimit>1</RasConnectRetryLimit><!-- Unit is milliseconds --><RasConnectRetryTimerInterval>15000</RasConnectRetryTimerInterval><!-- For testing purposes --><UseGsmSimulator>true</UseGsmSimulator><!--<SmsQueueDelayTimerInterval>120000</SmsQueueDelayTimerInterval>--><SmsQueueDelayTimerInterval>10000</SmsQueueDelayTimerInterval><!-- Client Certificate Serial Number to load at run-time --><ClientCertSerialNumber>3e87b3af98f62a934d14e0edd468b7c8</ClientCertSerialNumber></CommSvcConfig></xmlSerializerSection></CommunicationSvcConfig>";
            XmlSerializer serializer = new XmlSerializer(typeof(CommunicationSvcConfig));
            using (StringReader reader = new StringReader(xml))
            {
                config = (CommunicationSvcConfig)(serializer.Deserialize(reader));
            }
            return config;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}