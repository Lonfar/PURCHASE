using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Topis.SystemFramework
{   /// <summary>
	///     A class to help with error checking and automatic logging
	///     of asserts and conditional checks. 
	///     <remarks>
	///         This class works with displays system assert dialogs 
	///         as well as writing to the application log with the 
	///         ApplicationLog class. There is no instance data associated 
	///         with this class.
	///     </remarks>
	/// </summary>
	public class ApplicationAssert
	{
#if !DEBUG
        /// <value>
        ///     A LineNumber constant to be used when not in a debug build
        ///     so that ApplicationAssert. LineNumber is always a valid expression.
        ///     <remarks>
        ///         This allows us to pass ApplicationAssert.LineNumber with good debug
        ///         functionality and minimal runtime overhead.
        ///     </remarks>
        /// </value>
        public const int LineNumber = 0;
#else
		/// <value>Property LineNumber is used to get the current line number in the calling function.</value>
		/// <remarks>
		///     This should be called in a parameter list to get accurate 
		///     information about the line number before the Check* functions 
		///     are called. If we wait until the Check* functions themselves 
		///     to retrieve this information, then the stack trace indicates 
		///     the next executable line, which is only marginally useful 
		///     information. This function is compiled out in debug builds in
		///     favor of the LineNumber constant.
		///     Returns LineNumber, or 0 on failure.
		/// </remarks>
		public static int LineNumber
		{
			get
			{
				try
				{
					//
					// Get the trace information with file information by skipping
					//   this function and then reading the top stack frame.
					//
					return (new StackTrace(1, true)).GetFrame(0).GetFileLineNumber();
				}
				catch
				{
				}

				return 0;
			}
		}
#endif
        
		/// <summary>
		///     Check the given condition and show an assert dialog when the
		///     desktop is interactive. 
		///     <remarks>
		///         Log the assertion at a warning level in case the desktop is not 
		///         interactive. The text will always contain full stack trace 
		///         information and will show the location of the error condition if 
		///         the source code is available.
		///     </remarks>
		///     <param name="condition">An expression to be tested for True</param>
		///     <param name="errorText">The message to display</param>
		///     <param name="lineNumber">
		///         The line of the current error in the function. See
		///         GenerateStackTrace for more information.
		///     </param>
		/// </summary>
		[ConditionalAttribute("DEBUG")]
		public static void Check(bool condition, String errorText, int lineNumber)
		{
			if ( !condition )
			{
				String detailMessage = String.Empty;
				StringBuilder strBuilder;
				GenerateStackTrace(lineNumber, out detailMessage);
				strBuilder = new StringBuilder();
				strBuilder.Append("Assert: ").Append("\r\n").Append(errorText).Append("\r\n").Append(detailMessage);
				ApplicationLog.WriteWarning(strBuilder.ToString());
				System.Diagnostics.Debug.Fail(errorText, detailMessage);
			}
		}
        
		/// <summary>
		///     Verify that a required condition holds. 
		///     <remarks>
		///         Show an assert dialog in a DEBUG build before throwing an 
		///         ApplicationException. It is assumed that the exception will be 
		///         handled or logged, so this does not log a warning for the assertion 
		///         like the Check function, which does not actually throw.
		///     </remarks>
		///     <param name="condition">An expression to be tested for True</param>
		///     <param name="errorText">The message to display</param>
		///     <param name="lineNumber">
		///         The line of the current error in the function. See 
		///         GenerateStackTrace for more information.
		///     </param>                        
		///     <exception class="System.ApplicationException">
		///         The checked condition failed.    
		///     </exception>
		/// </summary>
		public static void CheckCondition(bool condition, String errorText, int lineNumber)
		{
			//Test the condition
			if ( !condition )
			{
				//Assert and throw if the condition is not met
				String detailMessage;
				GenerateStackTrace(lineNumber, out detailMessage);
				Debug.Fail(errorText, detailMessage);
        
				throw new ApplicationException(errorText);
            
			}
		}
        
		/// <summary>
		///     Generate a stack trace to display/log with the assertion text.
		///     <remarks>
		///         The trace information includes file and line number information
		///         if its available, as well as a copy of the line of text if
		///         the source code is available. This function is only included in
		///         DEBUG builds of the application.
		///     </remarks>
		///     <param name="lineNumber">
		///         The line of the current error in the function. This                  
		///         value should be retrieved by call Application.LineNumber             
		///         in the parameter list of any of the Check* functions. If         
		///         LineNumber is not provided,then the next executable line is used.
		///     </param>
		///     <param name="currentTrace">Returns the generated stack trace.</param>
		/// </summary>
		[ConditionalAttribute("DEBUG")]
		private static void GenerateStackTrace(int lineNumber, out String currentTrace)
		{
			currentTrace = String.Empty;
            
#if DEBUG
			StringBuilder message; //Used for smart string concatenation
			String        fileName;       //The source file name
			int           currentLine;       //The line to process in the source file
			String        sourceLine;     //The line from the source file
			StreamReader  fileStream = null; //The reader used to scan the source file
			bool          openedFile = false;
			StackTrace    curTrace;
			StackFrame    curFrame;
             
			message = new StringBuilder();
            
			//New StackTrace should never fail, but Try/Catch to be rock solid.
			try
			{
        
				//Get a new stack trace with line information. Skip the first function
				// and second functions (this one, and the calling Check* function)
				curTrace = new StackTrace(2, true);
				try
				{        
					//
					// Get the first retrieved stack frame and attempt to get
					//   file information from the trace, then open the file
					//   and find the specified line. Display as much information
					//   as possible if this is not supported.
					//
					curFrame = curTrace.GetFrame(0);
        
					//Retrieve and add File/Line information. Note that we only
					//proceed if both of these are available.
					if ((String.Empty != (fileName = curFrame.GetFileName())) && 
						(0 <= (currentLine = (lineNumber != 0) ? lineNumber : curFrame.GetFileLineNumber())))
					{
						//Append File name and line number
						message.Append(fileName).Append(", Line: ").Append(currentLine);
            
						//Append the actual code if we can find the source file
						fileStream = new StreamReader(fileName);
						openedFile = true;
						do
						{
							sourceLine = fileStream.ReadLine();
							--currentLine;
						} while (currentLine != 0);
        
						message.Append("\r\n");
                        
						if (lineNumber != 0)
						{
							message.Append("Current executable line:");
						}
						else
						{
							message.Append("\r\n").Append("Next executable line:");
						}
                                
						message.Append("\r\n").Append(sourceLine.Trim());
					}
				}
				catch
				{
					//Ignore errors, just show as much as we can
				}
				finally
				{
					//Always close the file
					if (openedFile) fileStream.Close();
				}
            
				//Retrieve the final string
				currentTrace = message.ToString();
			}
			catch
			{
				//Nothing to do, just get out of here with the default (empty) return value
			}
#endif
		}
    
	}
}
