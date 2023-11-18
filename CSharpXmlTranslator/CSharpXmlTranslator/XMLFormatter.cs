using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpXmlTranslator
{
    public class XMLFormatter
    {

        static string ConvertingObjectClassName = null;
        static bool mainObjectNameInitialize = true;

        public static string Convert(object obj)
        {
            if (obj == null) { return ""; }

            string final_string = Convert2(obj);

            return final_string + "</" + ConvertingObjectClassName + ">" + "\n";
        }
        public static string Convert2(object obj)
        {
            if (obj == null) { return ""; }

            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = obj.GetType().GetProperties();

            if (mainObjectNameInitialize)
            {
                ConvertingObjectClassName = obj.GetType().Name;
                //Console.WriteLine(ConvertingObjectClassName);
                mainObjectNameInitialize = false;
                sb.Append("<" + ConvertingObjectClassName + ">" + "\n");
            }


            foreach (PropertyInfo pi in properties)
            {

                //object value = pi.GetValue(obj);
                if (pi.GetValue(obj) is string || pi.PropertyType.IsPrimitive
                    //|| pi.GetValue(obj) is double
                    //|| pi.GetValue(obj) is float
                    //|| pi.GetValue(obj) is int
                    //|| pi.GetValue(obj) is long
                    //|| pi.GetValue(obj) is ulong
                    //|| pi.GetValue(obj) is decimal
                    )
                {
                    sb.Append("<" + pi.Name + ">" + pi.GetValue(obj) + "</" + pi.Name + ">" + "\n");
                }
                else if (pi.GetValue(obj) is DateTime)
                {
                    var values = (DateTime)pi.GetValue(obj);
                    //string value = dateTime.ToString("MM\\/dd\\/yyyy h:mm:ss tt");
                    sb.Append("<" + pi.Name + ">" + values + "</" + pi.Name + ">" + "\n");
                }
                else if (pi.GetValue(obj) is Array)
                {
                    IList list = pi.GetValue(obj) as Array;
                    sb.Append("<" + pi.Name + ">" + "\n");
                    for (int i = 0; i < list.Count; i++)
                    {
                        sb.Append("<" + list.GetType().GetElementType().Name + ">" + "\n");
                        sb.Append(Convert2(list[i]));
                        sb.Append("</" + list.GetType().GetElementType().Name + ">" + "\n");

                    }
                    sb.Append("</" + pi.Name + ">" + "\n");

                }

                else if (pi.GetValue(obj) is IList)
                {
                    IList list = pi.GetValue(obj) as IList;
                    sb.Append("<" + pi.Name + ">" + "\n");
                    for (int i = 0; i < list.Count; i++)
                    {
                        sb.Append("<" + list.GetType().GetGenericArguments()[0].Name + ">" + "\n");
                        sb.Append(Convert2(list[i]));
                        sb.Append("</" + list.GetType().GetGenericArguments()[0].Name + ">" + "\n");

                    }
                    sb.Append("</" + pi.Name + ">" + "\n");

                }


                else if (pi.PropertyType.IsClass)
                {
                    sb.Append("<" + pi.Name + ">" + "\n");
                    sb.Append(Convert2(pi.GetValue(obj)));
                    sb.Append("</" + pi.Name + ">" + "\n");

                }

                else
                {
                    sb.Append("<" + pi.Name + ">" + pi.GetValue(obj) + "</" + pi.Name + ">" + "\n");
                }


            }


            return sb.ToString();




        }



    }
}
