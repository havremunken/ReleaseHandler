using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReleaseHandlerLibrary
{
    public class ReleaseBundle
    {
        public string Name { get; set; }
        public Version Version { get; set; }
        public VersionField Field { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as ReleaseBundle;
            if (other == null)
                return false;

            if (!Name.Equals(other.Name))
                return false;

            if (!Version.Equals(other.Version))
                return false;

            return Field.Equals(other.Field);
        }
    }
}
