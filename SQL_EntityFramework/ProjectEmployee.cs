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
    
    public partial class ProjectEmployee
    {
        public int ProjectEmployee_ID { get; set; }
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public int IsManager { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }
}
