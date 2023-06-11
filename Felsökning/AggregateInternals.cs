// ----------------------------------------------------------------------
// <copyright file="AggregateInternals.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AggregateInternals"/> class.
    /// </summary>
    internal sealed class AggregateInternals : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregateInternals"/> class.
        /// </summary>
        public AggregateInternals()
        {
            this.HResults = new List<string>(0);
            this.Messages = new List<string>(0);
            this.StackTraces = new List<string>(0);
            this.ReturnStrings = Array.Empty<string>();
        }

        /// <summary>
        ///     Gets or sets the value of the <see cref="HResults"/> list, which is used to maintain
        ///     a list of hresults to return back to the caller.
        /// </summary>
        private List<string> HResults { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="Messages"/> list, which is used to maintain
        ///     a list of the messages to return back to the caller.
        /// </summary>
        private List<string> Messages { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StackTraces"/> list, which is used to maintain
        ///     a list of the stack traces to return back to the caller.
        /// </summary>
        private List<string> StackTraces { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="ReturnStrings"/> string array, which is used to
        ///     return data back to the caller.
        /// </summary>
        private string[] ReturnStrings { get; set; }

        /// <summary>
        ///     Used to expose the private method and returns for the aggregate exception processing.
        /// </summary>
        /// <param name="exception">The exception to be processed.</param>
        /// <returns>A string array of the hResults, Messages, and StackTraces.</returns>
        public string[] DelveInternally(Exception exception)
        {
            this.InternalUnbox(exception);
            return this.ReturnStrings;
        }

        /// <summary>
        ///     We dispose of the things that we no longer need.
        /// </summary>
        public void Dispose()
        {
            this.StackTraces.Clear();
            this.Messages.Clear();
            this.HResults.Clear();
            this.ReturnStrings = new string[] { };
            GC.ReRegisterForFinalize(this.StackTraces);
            GC.ReRegisterForFinalize(this.Messages);
            GC.ReRegisterForFinalize(this.HResults);
            GC.ReRegisterForFinalize(this.ReturnStrings);
        }

        /// <summary>
        ///     Internally processes the Aggregrate Exception down to the Exceptions we care about and returns a set of strings from that.
        /// </summary>
        /// <param name="exception">The exception to be unboxed.</param>
        private void InternalUnbox(Exception exception)
        {
            if (exception.HResult != 0)
            {
                if (!this.HResults.Exists(r => r == exception.HResult.ToString()))
                {
                    this.HResults.Add(exception.HResult.ToString());
                }
            }

            if (!string.IsNullOrWhiteSpace(exception.StackTrace))
            {
                string[] stringSeparators = new string[] { "\r\n" };
                string[] strings = exception.StackTrace.Split(stringSeparators, StringSplitOptions.None);
                foreach (string s in strings)
                {
                    this.StackTraces.Add(s);
                }
            }

            if (exception.Message != null && !string.IsNullOrWhiteSpace(exception?.Message.ToString()))
            {
                this.Messages.Add(exception.Message.ToString());
            }

            if (exception?.GetType() == typeof(AggregateException))
            {
                AggregateException? aggregateException = exception as AggregateException;
                if (aggregateException?.InnerExceptions != null && aggregateException.InnerExceptions.Count != 0)
                {
                    foreach (Exception wrapped in aggregateException.InnerExceptions)
                    {
                        this.InternalUnbox(wrapped);
                    }
                }
            }
            else
            {
                if (exception?.InnerException != null)
                {
                    this.InternalUnbox(exception.InnerException);
                }
            }

            // Now we're down to the raw stuff, so let's work with it.
            this.Messages.Reverse();
            this.StackTraces.Reverse();

            string[] stackStrings = this.StackTraces.ToArray();
            string[] messageStrings = this.Messages.ToArray();
            string[] hresultStrings = this.HResults.ToArray();

            StringBuilder stackTraceStringBuilder = new StringBuilder();
            StringBuilder messagesStringBuilder = new StringBuilder();
            StringBuilder hresultsStringBuilder = new StringBuilder();

            foreach (string s in stackStrings)
            {
                stackTraceStringBuilder.AppendLine(s);
            }
            if (stackTraceStringBuilder.Length > 0)
            {
                stackTraceStringBuilder.AppendLine("--- End of stack trace from previous location where exception was thrown ---");
            }

            foreach (string s in messageStrings)
            {
                messagesStringBuilder.AppendLine(s);
            }

            foreach (string s in hresultStrings)
            {
                hresultsStringBuilder.AppendLine(s);
            }

            this.ReturnStrings = new string[]
            {
                hresultsStringBuilder.ToString(),
                messagesStringBuilder.ToString(),
                stackTraceStringBuilder.ToString(),
            };
        }
    }
}