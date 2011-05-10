
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;

using WcfTest;

namespace WcfTestClient
{

    public class Client
    {

        public static void Main(string[] args)
        {
            try
            {
                try
                {
                ChannelFactory<IWcfTest> factory = new ChannelFactory<IWcfTest>("RemoteService");
                IWcfTest service = factory.CreateChannel();

                //IList<Child> childList = service.GetGraph1();
                //Console.WriteLine(childList.Count);
                //Console.WriteLine(childList[0].Name + childList[0].Parent.Name);
                //Console.WriteLine(childList[1].Name + childList[1].Parent.Name);
                //Console.WriteLine(childList[0].Parent == childList[1].Parent);
                
                Parent parent = service.GetGraph2();
                Console.WriteLine(parent.Name);
                Console.WriteLine(parent.Children.Count);

                //Parent parent = new Parent("error");
                //Parent parent = new Parent("no error");
                //parent = service.GetGraph(parent);
                //Console.WriteLine(parent.Name);
                //Console.WriteLine(parent.Children.Count);
                }
                catch (FaultException fe)
                {
                    Type t = fe.GetType();
                    PropertyInfo detailInfo = t.GetProperty("Detail");
                    object detail = detailInfo.GetValue(fe, null);

                    if (detail != null)
                    {
                        throw (Exception) detail;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (MyException myException)
            {
                Console.WriteLine("Caught MyException info=" + myException.Info);

                Type t = myException.GetType();
                Console.WriteLine(t.BaseType.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Done");
                Console.ReadLine();
            }
        }

    }


}

