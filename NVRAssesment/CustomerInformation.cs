using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NvrAssessment
{
    /// <summary>
    ///     Represents customer information
    /// </summary>
    public sealed class CustomerInformation
    {   
        /// <summary>
        ///     First name
        /// </summary>
        [CanBeNull]
        public string FirstName { get; set; }

        /// <summary>
        ///     Last name
        /// </summary>
        [CanBeNull]
        public string LastName { get; set; }

        /// <summary>
        ///     Email address
        /// </summary>
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string EmailAddress { get; set; }

        /// <summary>
        ///     Phone number
        /// </summary>
        [CanBeNull]
        public string PhoneNumber { get; set; }

    
        public override bool Equals(object obj) => obj is CustomerInformation information
            && this.FirstName == information.FirstName
            && this.LastName == information.LastName
            && this.EmailAddress == information.EmailAddress
            && this.PhoneNumber == information.PhoneNumber;

        public override int GetHashCode() => HashCode.Combine(this.FirstName, this.LastName, this.EmailAddress, this.PhoneNumber);
    }
}
