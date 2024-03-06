[System.Serializable]

public class Dialogue
{
    public Part[] parts;

    public Part getPart(string id)
    {
        return System.Array.Find(parts, part => part.ID== id);
    }
    public class Part
    {
        public string ID;
        public string text;
        public string nextID;
        public Response[] responses;

        [System.Serializable]
        public class Response
        {
            public string ID;
            public string text;
        }
    }
}
