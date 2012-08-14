﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PubNub_Messaging
{
    public class Pubnub_Example
    {
        static public void Main()
        {
            
            Publish_Example();
            
            History_Example();
            
            Timestamp_Example();
            
            Subscribe_Example();
            
            Presence_Example();
            
            HereNow_Example();
            
            Console.ReadKey();
        }
        static void Publish_Example()
        {
            Pubnub pubnub = new Pubnub(
                    "demo",
                    "demo",
                    "",
                    false);
            string channel = "my/channel";
            string message = "Pubnub API Usage Example - Publish";

            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Publish")
                {
                    Console.WriteLine("*********** Publish Messages *********** ");
                    Console.WriteLine(
                        "Publish Success: " + ((Pubnub)sender).Publish[0].ToString() +
                        "\nPublish Info: " + ((Pubnub)sender).Publish[1].ToString()
                        );
                }
            };
            pubnub.publish(channel, message);
        }
        static void History_Example()
        {
            Pubnub pubnub = new Pubnub(
                    "demo",
                    "demo",
                    "",
                    false);
            string channel = "my/channel";
            
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "History")
                {
                    Console.WriteLine("*********** History Messages *********** ");
                    MessageFeeder(((Pubnub)sender).History);
                }
            };
            pubnub.history(channel, 10);
        }
        static void Timestamp_Example()
        {
            Pubnub pubnub = new Pubnub(
                    "demo",
                    "demo",
                    "",
                    false);
            
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Time")
                {
                    Console.WriteLine("********** Timestamp Messages ********** ");
                    MessageFeeder(((Pubnub)sender).Time[0]);
                }
            };
            pubnub.time();
        }
        static void Subscribe_Example()
        {
            Pubnub pubnub = new Pubnub(
                    "demo",
                    "demo",
                    "",
                    false);
            string channel = "my/channel";

            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "ReturnMessage")
                {
                    Console.WriteLine("********** Subscribe Messages ********** ");
                    MessageFeeder(((Pubnub)sender).ReturnMessage);
                }
            };
            pubnub.subscribe(channel);
        }

        static void Presence_Example()
        {
            Pubnub pubnub = new Pubnub(
                    "demo",
                    "demo",
                    "",
                    false);
            string channel = "my/channel";

            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Presence")
                {
                    Console.WriteLine("********** Presence Messages *********** ");
                    MessageFeeder(((Pubnub)sender).Presence);                   
                }
            };
            pubnub.presence(channel);
        }

        static void HereNow_Example()
        {
            Pubnub pubnub = new Pubnub(
                    "demo",
                    "demo",
                    "",
                    false);
            //string channel = "my/channel";
            string channel = "hello_world";

            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Here_Now")
                {
                    Console.WriteLine("********** Here Now Messages *********** ");
                    Dictionary<string, object> _message = (Dictionary<string, object>)(((Pubnub)sender).Here_Now[0]);
                    foreach (object uuid in (object[])_message["uuids"])
                    {
                        Console.WriteLine("UUID: " + uuid.ToString());
                    }
                    Console.WriteLine("Occupancy: " + _message["occupancy"].ToString());
                }
            };
            pubnub.here_now(channel);
        }

        static void MessageFeeder(List<object> feed)
        {
            foreach (object message in feed)
            {
                try
                {
                    Dictionary<string, object> _messageHistory = (Dictionary<string, object>)(message);
                    Console.WriteLine("Key: " + _messageHistory.ElementAt(0).Key + " - Value: " + _messageHistory.ElementAt(0).Value);
                }
                catch
                {
                    Console.WriteLine(message.ToString());
                }
            }
        }
        static void MessageFeeder(object feed)
        {
            try
            {
                Dictionary<string, object> _message = (Dictionary<string, object>)(feed);
                for (int i = 0; i < _message.Count; i ++)
                    Console.WriteLine("Key: " + _message.ElementAt(i).Key + " - Value: " + _message.ElementAt(i).Value);
            }
            catch
            {
                try
                {
                    List<object> _message = (List<object>)feed;
                    for (int i = 0; i < _message.Count; i++)
                        Console.WriteLine(_message[i].ToString());
                }
                catch
                {
                    Console.WriteLine("Time: " + feed.ToString());
                }
                
            }
        }
    }
}
