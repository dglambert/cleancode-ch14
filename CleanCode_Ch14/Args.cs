using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
//using static com.objectmentor.utilities.args.ArgsException.ErrorCode.*;
//using java.util.*;


namespace CleanCode_Ch14.utilities.args
{
    public class Args
    {
        private Dictionary<char, ArgumentMarshaler> marshalers;
        private HashSet<char> argsFound;
        private List<String>.Enumerator currentArgument;

        public Args(String schema, String[] args)
        {
            marshalers = new Dictionary<char, ArgumentMarshaler>();
            argsFound = new HashSet<char>();
            parseSchema(schema);
            parseArgumentStrings(Arrays.asList(args));
        }

        private void parseSchema(String schema)
        {
            foreach (String element in schema.Split(","))
            {
                if (element.Length > 0)
                {
                    parseSchemaElement(element.Trim());
                }
            }
        }

        private void parseSchemaElement(String element)
        {
            char elementId = element.ElementAt<Char>(0); 
            String elementTail = element.Substring(1);
            validateSchemaElementId(elementId);
            if (elementTail.Length == 0)
                marshalers.Add(elementId, new BooleanArgumentMarshaler());
            else if (elementTail.Equals("*"))
                marshalers.Add(elementId, new StringArgumentMarshaler());
            else if (elementTail.Equals("#"))
                marshalers.Add(elementId, new IntegerArgumentMarshaler());
            else if (elementTail.Equals("##"))
                marshalers.Add(elementId, new DoubleArgumentMarshaler());
            else if (elementTail.Equals("[*]"))
                marshalers.Add(elementId, new StringArrayArgumentMarshaler());
            else
                throw new ArgsException(INVALID_ARGUMENT_FORMAT, elementId, elementTail);
        }

        private void validateSchemaElementId(char elementId)
        {
            if (!Char.IsLetter(elementId))
                throw new ArgsException(INVALID_ARGUMENT_NAME, elementId, null);
        }

        //private void parseArgumentStrings(List<String> argsList)
        //{
        //    for (currentArgument = argsList.GetEnumerator(); currentArgument.;)
        //    {
        //        String argString = currentArgument.next();
        //        if (argString.StartsWith("-"))
        //        {
        //            parseArgumentCharacters(argString.Substring(1));
        //        }
        //        else
        //        {
        //            currentArgument.previous();
        //            break;
        //        }
        //    }
        //}

        //private void legacyParseArgumentStrings(List<String> argsList) throws ArgsException
        //{
        //    for (currentArgument = argsList.listIterator(); currentArgument.hasNext();)
        //    {
        //        String argString = currentArgument.next();
        //        if (argString.startsWith(“-”)) 
        //        {
        //            parseArgumentCharacters(argString.substring(1));
        //        } 
        //        else 
        //        {
        //            currentArgument.previous();
        //            break;
        //        }
        //    }
        //}

        private void parseArgumentCharacters(string argChars)
        {
            for (int i = 0; i < argChars.Length; i++)
                parseArgumentCharacter(argChars.charAt(i));
        }


    }





public class Args
{
    







private void parseArgumentCharacter(char argChar) throws ArgsException
{
    ArgumentMarshaler m = marshalers.get(argChar);
    if (m == null)
    {
        throw new ArgsException(UNEXPECTED_ARGUMENT, argChar, null);
    }
    else
    {
        argsFound.add(argChar);
        try
        {
            m.set(currentArgument);
        }
        catch (ArgsException e)
        {
            e.setErrorArgumentId(argChar);
            throw e;
        }
    }
    }
public boolean has(char arg)
{
    return argsFound.contains(arg);
}
public int nextArgument()
{
    return currentArgument.nextIndex();
}
public boolean getBoolean(char arg)
{
    return BooleanArgumentMarshaler.getValue(marshalers.get(arg));
}
public String getString(char arg)
{
    return StringArgumentMarshaler.getValue(marshalers.get(arg));
}
public int getInt(char arg)
{
    return IntegerArgumentMarshaler.getValue(marshalers.get(arg));
}
public double getDouble(char arg)
{
    return DoubleArgumentMarshaler.getValue(marshalers.get(arg));
}
public String[] getStringArray(char arg)
{
    return StringArrayArgumentMarshaler.getValue(marshalers.get(arg));
}
}
