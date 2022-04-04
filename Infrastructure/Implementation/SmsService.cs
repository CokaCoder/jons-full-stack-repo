using CSharpFunctionalExtensions;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Implementation
{
    public class SmsService : ISmsService
    {
        public List<Sms> ReceiveSms()
        {
            throw new NotImplementedException();
        }

        public Result SendSms(string body, string number)
        {
            string _URL = "192.168.1"; //MS Gateway
            string _senderid = "TESTTC";   // sender id 

            string _user = HttpUtility.UrlEncode("TestSMS"); // API user name to send SMS
            string _pass = HttpUtility.UrlEncode("123456");     // API password to send SMS
            string _recipient = HttpUtility.UrlEncode("9999999999");  // who will receive message
            string _messageText = HttpUtility.UrlEncode("testing sms..."); // text message

            // Creating URL to send sms
            string _createURL = _URL +
            "username =" + _user +
               "&pass=" + _pass +
               "&senderid=" + _senderid +
               "&dest_mobileno=" + _recipient +
               "&message=" + _messageText;

            try
            {
                // creating web request to send sms 
                HttpWebRequest _createRequest = (HttpWebRequest)WebRequest.Create(_createURL);
                // getting response of sms
                HttpWebResponse myResp = (HttpWebResponse)_createRequest.GetResponse();
                System.IO.StreamReader _responseStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = _responseStreamReader.ReadToEnd();
                _responseStreamReader.Close();
                myResp.Close();
            }
            catch
            {
                return Result.Failure("Couldn't send sms");
            }

            return Result.Success("Success");

        }
    }
}

