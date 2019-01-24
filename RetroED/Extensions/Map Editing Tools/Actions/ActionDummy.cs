namespace RetroED.Tools.MapEditor.Actions
{
    class ActionDummy : IAction
    {
        public string Description => string.Empty;

        public ActionDummy() { }
        public void Undo() { }
        public IAction Redo() { return this; }
    }
}
