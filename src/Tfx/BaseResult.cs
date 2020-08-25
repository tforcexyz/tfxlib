// Copyright 2017 T-Force Xyz
// Please refer to LICENSE & CONTRIB files in the project root for license information.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace Xyz.TForce
{

  public abstract class BaseResult
  {

    protected BaseResult()
    {
      Messages = new MessageCollection();
      Validations = new ValidationCollection();
    }

    public Exception Exception { get; set; }

    public MessageCollection Messages { get; private set; }

    public LogicResultState State
    {
      get
      {
        if (Exception != null)
        {
          return LogicResultState.Error;
        }
        if (!Validations.IsValidated)
        {
          return LogicResultState.Invalidation;
        }
        return LogicResultState.Success;
      }
    }

    public abstract long Type { get; }

    public ValidationCollection Validations { get; private set; }

    public void EnsureSuccess()
    {
      if (State == LogicResultState.Error)
      {
        Exception logicException = new LogicException(Exception);
        throw logicException;
      }
      if (State == LogicResultState.Invalidation)
      {
        Exception validationException = new ValidationException(Validations);
        throw validationException;
      }
    }

    public void EnsureSuccess(string errorMessage)
    {
      if (State == LogicResultState.Error)
      {
        Exception logicException = new LogicException(errorMessage, Exception);
        throw logicException;
      }
      if (State == LogicResultState.Invalidation)
      {
        Exception validationException = new ValidationException(errorMessage, Validations);
        throw validationException;
      }
    }
  }
}
