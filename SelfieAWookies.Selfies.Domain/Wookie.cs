using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SelfieAWookies.Selfies.Domain
{
    [Serializable]
    public class Wookie
    {
        #region Properties
        public int Id { get; set; }
        public required string Name { get; set; }

        //[JsonIgnore]    // ignore la serialization de cette propriété
        public List<Selfie>? Selfies { get; set; }

        #endregion

    }
}
