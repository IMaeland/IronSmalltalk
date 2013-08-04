﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IronSmalltalk.Compiler.SemanticNodes;
using IronSmalltalk.ExpressionCompiler.BindingScopes;
using IronSmalltalk.ExpressionCompiler.Visiting;
using IronSmalltalk.Runtime;
using IronSmalltalk.Runtime.Bindings;

namespace IronSmalltalk.ExpressionCompiler
{
    public sealed class ClassMethodCompiler : MethodCompiler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtime">Smalltalk runtime responsible for running the code.</param>
        /// <param name="compilerOptions">Options that control the workings of the compiler.</param>
        public ClassMethodCompiler(SmalltalkRuntime runtime, CompilerOptions compilerOptions)
            : base(runtime, compilerOptions)
        {
        }

        protected override VisitingContext GetVisitingContext(MethodNode parseTree, SmalltalkClass cls, Expression self, Expression executionContext, Expression[] arguments)
        {
            SmalltalkNameScope globalNameScope = this.CompilerOptions.GlobalNameScope ?? this.Runtime.GlobalScope;

            BindingScope globalScope = BindingScope.ForClassMethod(cls, globalNameScope);
            BindingScope reservedScope = ReservedScope.ForClassMethod();

            return new VisitingContext(this, globalScope, reservedScope, self, executionContext, arguments, cls.Name, parseTree.Selector);
        }
    }
}
