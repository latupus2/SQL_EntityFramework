//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SQL_EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            this.ProjectEmployee = new HashSet<ProjectEmployee>();
        }
    
        public int Project_ID { get; set; }
        public string Project_Name { get; set; }
        public string Project_ClientCompany { get; set; }
        public string Project_ExecutorCompany { get; set; }
        public string Project_StartDate { get; set; }
        public string Project_EndDate { get; set; }
        public int Project_Priority { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectEmployee> ProjectEmployee { get; set; }
    }
}
