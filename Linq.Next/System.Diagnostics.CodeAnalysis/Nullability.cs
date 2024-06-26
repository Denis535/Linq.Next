﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if NETSTANDARD2_0
namespace System.Diagnostics.CodeAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;

// AllowNull
[AttributeUsage( AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false )]
internal sealed class AllowNullAttribute : Attribute {
}
// DisallowNull
[AttributeUsage( AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false )]
internal sealed class DisallowNullAttribute : Attribute {
}

// MaybeNull
[AttributeUsage( AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false )]
internal sealed class MaybeNullAttribute : Attribute {
}
// MaybeNull/When
[AttributeUsage( AttributeTargets.Parameter, Inherited = false )]
internal sealed class MaybeNullWhenAttribute : Attribute {
    public bool ReturnValue { get; }
    public MaybeNullWhenAttribute(bool returnValue) {
        ReturnValue = returnValue;
    }
}

// NotNull
[AttributeUsage( AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false )]
internal sealed class NotNullAttribute : Attribute {
}
// NotNull/When
[AttributeUsage( AttributeTargets.Parameter, Inherited = false )]
internal sealed class NotNullWhenAttribute : Attribute {
    public bool ReturnValue { get; }
    public NotNullWhenAttribute(bool returnValue) {
        ReturnValue = returnValue;
    }
}
// NotNull/IfNotNull
[AttributeUsage( AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false )]
internal sealed class NotNullIfNotNullAttribute : Attribute {
    public string ParameterName { get; }
    public NotNullIfNotNullAttribute(string parameterName) {
        ParameterName = parameterName;
    }
}

// MemberNotNull
[AttributeUsage( AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true )]
internal sealed class MemberNotNullAttribute : Attribute {
    public string[] Members { get; }
    public MemberNotNullAttribute(string member) {
        Members = new[] { member };
    }
    public MemberNotNullAttribute(params string[] members) {
        Members = members;
    }
}
// MemberNotNull/When
[AttributeUsage( AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true )]
internal sealed class MemberNotNullWhenAttribute : Attribute {
    public bool ReturnValue { get; }
    public string[] Members { get; }
    public MemberNotNullWhenAttribute(bool returnValue, string member) {
        ReturnValue = returnValue;
        Members = new[] { member };
    }
    public MemberNotNullWhenAttribute(bool returnValue, params string[] members) {
        ReturnValue = returnValue;
        Members = members;
    }
}

// DoesNotReturn
[AttributeUsage( AttributeTargets.Method, Inherited = false )]
internal sealed class DoesNotReturnAttribute : Attribute {
}
// DoesNotReturn/If
[AttributeUsage( AttributeTargets.Parameter, Inherited = false )]
internal sealed class DoesNotReturnIfAttribute : Attribute {
    public bool ParameterValue { get; }
    public DoesNotReturnIfAttribute(bool parameterValue) {
        ParameterValue = parameterValue;
    }
}
#endif
