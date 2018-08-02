using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace report_ankapur
{
    public class GetConnectionString
    {
        string sqlConnectionString;
        public string CustomizeConnectionString(string restcode)
        {
              
                if (restcode == "HN")
                {
                   sqlConnectionString = @"Integrated Security=False;Initial Catalog=ankapur;Data Source=183.82.59.250;User ID=hytuser;Password=user1234;Connect Timeout=60"; //for server
            }
                else if (restcode == "KP")
                {
                   sqlConnectionString = @"Integrated Security=False;Initial Catalog=ankapur;Data Source=122.175.58.248;User ID=ankakpuser;Password=123456;Connect Timeout=60"; //for server

                }
                else if (restcode == "AN")
                {
                    sqlConnectionString = @"Integrated Security=False;Initial Catalog=ankapur;Data Source=122.175.55.141;User ID=ankapurasr;Password=asrao123;Connect Timeout=60"; //for server

                }

                return sqlConnectionString;
        }
    }
}