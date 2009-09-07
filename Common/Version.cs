/*
 // VersionManage v = new VersionManage();
 // v++;
 // Console.WriteLine(v.Version);
 //string s = "5.2.5.3";
 //Regex rx = new Regex(@"^(\d)\.(\d)\.(\d)\.(\d)$");
 //Match m = rx.Match(s);
 //for (int i = 1; i < 5; i++)
 //{
 //    Console.WriteLine(m.Groups[i].Value);
 //}
 VersionManage vm =VersionManage.MaxVersion;
 vm++;
 //VersionManage vm2 = new VersionManage("4.4.4.4");
 //vm = vm + vm2;
 //int a, b, c, d;
 //a = b = c = d = 0;
 Console.WriteLine(vm.Version);
 Console.ReadLine();
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public class Version
    {
        public static readonly Version MaxVersion = new Version("9.9.9.9");
        public static readonly Version MinVersion = new Version("0.0.0.0");

        UInt16 _v1;
        UInt16 _v2;
        UInt16 _v3;
        UInt16 _v4;
        Regex rx = new Regex(@"^(\d)\.(\d)\.(\d)\.(\d)$");

        public string CurrentVersion
        {
            get
            {
                return string.Format("{0}.{1}.{2}.{3}", _v1, _v2, _v3, _v4);
            }
            set
            {
                if (rx.IsMatch(value))
                {
                    Match m = rx.Match(value);
                    _v1 = UInt16.Parse(m.Groups[1].Value);
                    _v2 = UInt16.Parse(m.Groups[2].Value);
                    _v3 = UInt16.Parse(m.Groups[3].Value);
                    _v4 = UInt16.Parse(m.Groups[4].Value);
                }
                else
                {
                    init(0, 0, 0, 0);
                }
            }
        }

        public UInt16 V1
        {
            get
            {
                return _v1;
            }
        }
        public UInt16 V2
        {
            get
            {
                return _v2;
            }
        }
        public UInt16 V3
        {
            get
            {
                return _v3;
            }
        }
        public UInt16 V4
        {
            get
            {
                return _v4;
            }
        }
        private void init(UInt16 v1, UInt16 v2, UInt16 v3, UInt16 v4)
        {
            _v1 = v1;
            _v2 = v2;
            _v3 = v3;
            _v4 = v4;
        }

        public Version()
        {
            init(0, 0, 0, 0);
        }
        public Version(string version)
        {
            CurrentVersion = version;
        }
        public Version(UInt16 v1, UInt16 v2, UInt16 v3, UInt16 v4)
        {
            init(v1, v2, v3, v4);
        }
        public static Version operator ++(Version vm)
        {
            int u_vm1 = int.Parse(vm.CurrentVersion.Replace(".", ""));
            int u_vm2 = u_vm1 + 1;
            string str_vm2 = u_vm2.ToString();
            int len = str_vm2.Length;
            str_vm2 = str_vm2.Substring(len - 4);
            str_vm2 = string.Format("{0}.{1}.{2}.{3}", str_vm2[0], str_vm2[1], str_vm2[2], str_vm2[3]);
            return new Version(str_vm2);
        }
        public static Version operator +(Version vm1, Version vm2)
        {
            int u_vm1 = int.Parse(vm1.CurrentVersion.Replace(".", ""));
            int u_vm2 = int.Parse(vm2.CurrentVersion.Replace(".", ""));
            int u_vm3 = u_vm1 + u_vm2;
            string str_vm3 = u_vm3.ToString();
            int len = str_vm3.Length;
            str_vm3 = str_vm3.Substring(len - 4);
            str_vm3 = string.Format("{0}.{1}.{2}.{3}", str_vm3[0], str_vm3[1], str_vm3[2], str_vm3[3]);
            return new Version(str_vm3);
        }
    }
}
