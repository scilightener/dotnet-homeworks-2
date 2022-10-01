.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser extends System.Object
{
    .method public static void ParseCalcArguments(
        string[] args,
        [out] float64& val1,
        [out] valuetype Hw2.CalculatorOperation& operation,
        [out] float64& val2) cil managed
    {
        ldarg.0
        call            bool Hw2.Parser::IsArgLengthSupported(string[])
        brfalse.s       argException
        
        // push args
        ldarg.0
        ldc.i4.0
        // get args[0]
        ldelem.ref
        ldarg.1
        call            bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s       argException
        
        // the same
        ldarg.0
        ldc.i4.2
        ldelem.ref
        ldarg.3
        call            bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s       argException
        
        ldarg.2
        ldarg.0
        ldc.i4.1
        ldelem.ref
        call            valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i4
        
        // check if operation is undefined
        ldarg.2
        ldind.i4
        ldc.i4.4
        ceq
        brtrue.s        operException
        
        ret
        
        argException:   newobj instance void [System.Runtime]System.ArgumentException::.ctor()
                        throw
                        
        operException:  newobj instance void [System.Runtime]System.InvalidOperationException::.ctor()
                        throw
    }
  
    .method private static bool IsArgLengthSupported(
        string[] args) cil managed
    {
        // push args
        ldarg.0
        // push args.Length
        ldlen
        // push 3
        ldc.i4.3
        // push args.Length == 3
        ceq
        ret
    }
  
    .method private static valuetype Hw2.CalculatorOperation ParseOperation(
          string arg) cil managed
    {
        .locals init ([0] valuetype Hw2.CalculatorOperation)
        
        // understandable
        ldarg.0
        ldstr       "+"
        call        bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s    addCase
        
        ldarg.0
        ldstr       "-"
        call        bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s    subCase
        
        ldarg.0
        ldstr       "*"
        call        bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s    mulCase
        
        ldarg.0
        ldstr       "/"
        call        bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s    divCase
        
        br.s        undefined
        
        addCase:    ldc.i4.0
                    br.s        castAndRet
        
        subCase:    ldc.i4.1
                    br.s        castAndRet

        mulCase:    ldc.i4.2
                    br.s        castAndRet
                    
        divCase:    ldc.i4.3
                    br.s        castAndRet
                    
        undefined:  ldc.i4.4
                    br.s        castAndRet
                    
        castAndRet: stloc.0
                    ldloc.0
                    ret
        // have a nice day
    }
}
