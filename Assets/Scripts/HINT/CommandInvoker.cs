using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private Stack<ICommand> _commandStack = new Stack<ICommand>();
    
    public void ExecuteCommand(ICommand command)
    {
        Debug.Log("Execute Command");
        command.Execute();
        _commandStack.Push(command);  // Push the executed command onto the stack
    }
    
    public void Undo()
    {
        if (_commandStack.Count > 0)
        {
            Debug.Log("Undo Command");
            ICommand command = _commandStack.Pop();  // Pop the last executed command from the stack
            command.Undo();
        }
    }
}