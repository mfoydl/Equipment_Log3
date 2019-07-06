using System;
using System.Collections.Generic;
using System.Reflection;

namespace Equipment_Log2 {
    class LogViewModel {
        private static Log saved;
        private static List<Log> logs;
        public static Log CurrentLog { get { return Log; } set { Log = value; } }


        internal static Log Log { get; set; }

        public LogViewModel() {
            Log = new Log();
            logs = JsonBuilder.Load();
            if (logs == null) {
                logs = new List<Log>();
            }
        }

        public static void Load(DateTime date, string shift) {
            if (Log.Submitted == false) {
                SaveLog();
            }

            if (saved != null && date == saved.Date && shift == saved.Shift) {
                LoadLog(saved);
            }
            else {
                LoadLog(FindLog(date,shift));
            }



        }
        private static Log FindLog(DateTime date, string shift) {
            Log newLog = null;
            foreach (Log tlog in logs) {
                if (tlog.Date == date && tlog.Shift.Equals(shift))
                    newLog = tlog;
            }
            if (newLog == null) {
                newLog = new Log {
                    Shift = shift,
                    Date = date,
                    Submitted = true
                };
            }
            return newLog;
        }

        public static void InitalLoad(DateTime date, string shift) {
            Log newLog=null;
            foreach (Log tlog in logs) {
                if (tlog.Date == date && tlog.Shift.Equals(shift))
                    newLog = tlog;
            }
            if (newLog == null) {
                newLog = new Log {
                    Shift = shift,
                    Date = date,
                    Submitted = false
                };
            }
            LoadLog(newLog);
            
        }
        private static void SaveLog() {
            saved = new Log();

            Type typeSource = saved.GetType();

            object objTarget = saved;

            PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);



            //Assign all source property to taget object 's properties

            foreach (PropertyInfo property in propertyInfo) {
                if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String))) {

                    property.SetValue(objTarget, property.GetValue(Log, null), null);

                }
            }
        }
        private static void LoadLog(Log newLog) {
            Type typeSource = newLog.GetType();

            object objTarget = Log;

            PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);



            //Assign all source property to taget object 's properties

            foreach (PropertyInfo property in propertyInfo) {
                if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String))) {

                    property.SetValue(objTarget, property.GetValue(newLog, null), null);

                }
            }
        }
        public static void Submit() {
            if (Log.Submitted == true)
                logs.Remove(Log);
            else
                Log.Submitted = true;
            logs.Add(Log);
            JsonBuilder.Save(logs);
        }

        public static string FindShift() {
            return Log.FindShift();
        }
    }
}
