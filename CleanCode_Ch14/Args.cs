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
        private LinkedListNode<String> currentArgument;

        public Args(String schema, String[] args)
        {
            marshalers = new Dictionary<char, ArgumentMarshaler>();
            argsFound = new HashSet<char>();
            parseSchema(schema);
            parseArgumentStrings(args.ToList());
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

        private void parseArgumentStrings(LinkedList<String> argsList)
        {
            for (currentArgument = argsList.First(); currentArgument.;)
            {
                String argString = currentArgument.next();
                if (argString.StartsWith("-"))
                {
                    parseArgumentCharacters(argString.Substring(1));
                }
                else
                {
                    currentArgument.previous();
                    break;
                }
            }
        }

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
            {
                parseArgumentCharacter(argChars[i]);
            }
        }

        private void parseArgumentCharacter(char argChar)
        {
            ArgumentMarshaler m = null;
            marshalers.TryGetValue(argChar, m);
            if (m == null)
            {
                throw new ArgsException(UNEXPECTED_ARGUMENT, argChar, null);
            }
            else
            {
                argsFound.Add(argChar);
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

        public bool has(char arg)
        {
            return argsFound.Contains(arg);
        }
        public int nextArgument()
        {
            return currentArgument.nextIndex();
        }
        public bool getBoolean(char arg)
        {
            ArgumentMarshaler m = null;
            marshalers.TryGetValue(arg, m);
            return BooleanArgumentMarshaler.getValue(m);
        }
        public String getString(char arg)
        {
            ArgumentMarshaler m = null;
            marshalers.TryGetValue(arg, m);
            return StringArgumentMarshaler.getValue(m);
        }
        public int getInt(char arg)
        {
            ArgumentMarshaler m = null;
            marshalers.TryGetValue(arg, m);
            return IntegerArgumentMarshaler.getValue(m);
        }
        public double getDouble(char arg)
        {
            ArgumentMarshaler m = null;
            marshalers.TryGetValue(arg, m);
            return DoubleArgumentMarshaler.getValue(m);
        }
        public String[] getStringArray(char arg)
        {
            ArgumentMarshaler m = null;
            marshalers.TryGetValue(arg, m);
            return StringArrayArgumentMarshaler.getValue(m);
        }

    }





public class Args
{
    









}
