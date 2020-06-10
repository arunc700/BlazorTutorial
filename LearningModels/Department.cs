using System;
using System.ComponentModel.DataAnnotations;

namespace LearningModels
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage ="Department is required")]
        public string DepartmentName { get; set; }
    }
}
