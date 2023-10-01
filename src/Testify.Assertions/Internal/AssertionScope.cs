namespace Testify.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal sealed class AssertionScope
{
    private static readonly AsyncLocal<AssertionScope> Current = new();
    private static readonly AssertionScope Root = new();

    private readonly AssertionScope? _parent;
    private readonly string? _message;
    private readonly List<string> _failureMessages = new();

    private AssertionScope()
        : this(null, null)
    {
    }

    private AssertionScope(AssertionScope? parent, string? message)
    {
        _parent = parent;
        _message = message;
    }

    public static void Fail(string message, AssertionScope? scope = null)
    {
        scope ??= Current.Value ?? Root;
        if (scope._parent == null)
        {
            throw FrameworkAdapter.CreateException(message);
        }
        else
        {
            scope._failureMessages.Add(message);
        }
    }

    public static void Push(string message)
    {
        Current.Value = new(Current.Value ?? Root, message);
    }

    public static void Pop(bool shouldThrow = true)
    {
        var scope = Current.Value ?? Root;
        if (scope == Root)
        {
            throw new InvalidOperationException("Assertion scope stack is empty.");
        }

        var (parent, message) = (scope._parent!, scope._message!);
        if (shouldThrow && scope._failureMessages.Any())
        {
            var lines = scope._failureMessages.SelectMany(f => f.Split(Environment.NewLine));
            var childFailures = string.Join(Environment.NewLine, lines.Select(line => $"   {line}"));
            Fail($"{scope._message}{Environment.NewLine}{childFailures}", parent);
        }

        Current.Value = parent;
    }
}
