﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SqueakyCleanEnergy.Models
{
    class ProjectTask
    {
        [JsonProperty("projectTaskId")]
        public Guid ProjectTaskId { get; set; }

        [JsonProperty("task")]
        public string Task { get; set; }

        [JsonProperty("startDate")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTimeOffset EndDate { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("projectId")]
        public Guid ProjectId { get; set; }
    }
}