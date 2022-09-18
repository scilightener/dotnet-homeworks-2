.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator extends System.Object
{
    .method public static float64 Calculate(
        float64 value1,
        valuetype Hw2.CalculatorOperation operation,
        float64 value2) cil managed
    {
        // push value1 onto the stack
        ldarg.0
        // push value2 onto the stack
        ldarg.2
        
        // push operation onto the stack
        ldarg.1
        // switch the operation
        switch (
                addFunc,
                subFunc,
                mulFunc,
                divFunc,
                undefined)
                
        // case CalculatorOperation.Plus
        addFunc:    add
                    ret
                    
        // case CalculatorOperation.Minus
        subFunc:    sub
                    ret
                    
        // case CalculatorOperation.Multiply
        mulFunc:    mul
                    ret
                    
        // case CalculatorOperation.Divide
        divFunc:    div
                    ret
                    
        // case _
        undefined:  newobj instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor()
                    throw
    }
}
