using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SqueakyCleanEnergy.Models
{
    class Project
    {
        [JsonProperty("projectId")]
        public Guid ProjectId { get; set; }

        [JsonProperty("projectName")]
        public string ProjectName { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("projectTasks")]
        public List<object> ProjectTasks { get; set; }

        [JsonIgnore]
        private string _statusImage;
        public string StatusImage
        {
            get
            {
                if (Status)
                    _statusImage = "done.png";

                return "in_progress.png";
            }
            set => _statusImage = value;
        }
    }
}
