#if CAN_SAVE_VB

using com.absence.variablesystem.banksystembase;
using com.absence.variablesystem.builtin;
using System.Collections.Generic;

namespace com.absence.savesystem.variablebanks
{
    [System.Serializable]
    public class TempVariableBankData
    {
        public int IntCount = 0;
        public int FloatCount = 0;
        public int StringCount = 0;
        public int BooleanCount = 0;

        public List<IntegerVariable> Ints = new();
        public List<FloatVariable> Floats = new();
        public List<StringVariable> Strings = new();
        public List<BooleanVariable> Booleans = new();

        public TempVariableBankData()
        {
        }

        public TempVariableBankData(VariableBank copyFrom)
        {
            Ints = new(copyFrom.Ints);
            Floats = new(copyFrom.Floats);
            Strings = new(copyFrom.Strings);
            Booleans = new(copyFrom.Booleans);

            IntCount = Ints.Count;
            FloatCount = Floats.Count;
            StringCount = Strings.Count;
            BooleanCount = Booleans.Count;
        }
    }

}

#endif