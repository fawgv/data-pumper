﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quirco.DataPumper
{
    /// <summary>
    /// Модель результата переливки
    /// </summary>
    public class DataPumperLog
    {
        public JobLog[] Logs { get; set; }
    }

    public class JobLog
    {
        /// <summary>
        /// Имя задания
        /// </summary>
        [JsonProperty("Job")]
        public string Name { get; set; }

        public long RecordsProcessed { get; set; }

        public TimeSpan ElapsedTime { get; set; }

        public DateTime ActualDate { get; set; }

        public string Error { get; set; }
    }
}