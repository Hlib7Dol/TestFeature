namespace BusWebApi.Helpers
{
    /// <summary>
    /// Storage caretaker
    /// </summary>
    public class StorageCaretaker : IStorageCaretaker
    {
        /// <summary>
        /// <see cref="IStorageMemento"/> object
        /// </summary>
        public IStorageMemento Memento { get; set; }
    }
}
