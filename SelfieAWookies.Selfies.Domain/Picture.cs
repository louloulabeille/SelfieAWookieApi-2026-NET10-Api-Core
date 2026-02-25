using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookies.Selfies.Domain
{
    public class Picture
    {
        #region Properties
    
            public Guid Id { get; set; }
            public required string Url { get; set; }
            public List<Selfie>? Selfies { get; set; }

        #endregion
    }
}
