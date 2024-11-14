namespace Unit
{
    public struct UnitCreationArgs
    {
        public string Id { get; }
        public int OwnerLayer { get; }
        public int TargetLayerMask { get; }

        public UnitCreationArgs(string id, int ownerLayer, int targetLayerMask)
        {
            Id = id;
            OwnerLayer = ownerLayer;
            TargetLayerMask = targetLayerMask;
        }
    }
}