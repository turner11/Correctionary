using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// An Interface that raises events when information worth logging is available
    /// </summary>
    public interface ILoggable
    {
        /// <summary>
        /// Occurs when an event that is worth logging has occured.
        /// </summary>
        event EventHandler<LogArgs> onWorthLogging;
    }
}
