namespace Generator3.Generation.Code
{
    public class Constant
    {
        private readonly GirModel.Constant _constant;

        public string Name => _constant.Name;
        public string TypeName => _constant.Type.GetName();
        public string Value => GetValue();
        public string Code => $"public static { TypeName } { Name } = { Value };";

        public Constant(GirModel.Constant constant)
        {
            _constant = constant;
        }

        private string GetValue()
        {
            return _constant.Type switch
            {
                GirModel.ComplexType {Name: {} name} when name.EndsWith("Flags") => $"({name}) {_constant.Value}",
                GirModel.String => "\"" + _constant.Value + "\"",
                _ => _constant.Value
            };
        }
    }
}
