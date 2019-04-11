using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SqueakyCleanEnergy.Models
{
    public class Project
    {
        [JsonProperty("projectId")]
        public Guid ProjectId { get; set; }

        [JsonProperty("projectName")]
        public string ProjectName { get; set; }

        [JsonProperty("isDone")]
        public bool IsDone { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("projectTasks")]
        public List<object> ProjectTasks { get; set; }

        [JsonIgnore]
        private string _doneImage;
        public string DoneImage
        {
            get => IsDone ? "done.png" : "in_progress.png";
            set => _doneImage = value;
        }
    }
}
