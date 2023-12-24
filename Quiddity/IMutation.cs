using ZiZZi;

namespace Quiddity
{
    /// <summary>
    /// Mutation that can be applied to a quiddity or a cluster.
    /// </summary>
    public interface IMutation
    {
        /// <summary>
        /// Name of the mutation (eg. "Rename")
        /// </summary>
        string Name();

        /// <summary>
        /// Tells if this mutation is valid fto be applied.
        /// </summary>
        bool Valid();

        /// <summary>
        /// Information for this mutation to be applied.
        /// </summary>
        IBlox Information();
    }
}