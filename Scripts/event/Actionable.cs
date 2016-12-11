using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace PointAndClick.Event {

	public class ActionEventArgs<T> : System.EventArgs {
		public T Value { get; set; }
	}

	public class MoveToTargetEventArgs : System.EventArgs {
		public delegate void OnTargetReached();

		public OnTargetReached onTargetReached;

	}

	public class CollisionEventArgs : System.EventArgs {
		public Actionable Target  { get; set; }

	}

	public abstract class Actionable : MonoBehaviour, IPointerClickHandler {
		
		public abstract T Action<T> () where T : Object;

		public abstract void Execute();

		public void invokeAction<T>() where T : Object {
			ActionEventArgs<T> args = new ActionEventArgs<T> ();
			args.Value = Action<T>();
			MessageQueue.dispatchEvent (this, args);
		}

		public void invokeDeferredAction<T>() where T : Object {
			MoveToTargetEventArgs mttArgs = new MoveToTargetEventArgs();
			mttArgs.onTargetReached = new MoveToTargetEventArgs.OnTargetReached(invokeAction<T>);
			MessageQueue.dispatchEvent (this, mttArgs);
		}

		public void OnPointerClick(PointerEventData eventData) {
			Execute ();
		}

		public virtual void OnMouseUpAsButton() {
			Execute ();
		}

		public void OnCollisionEnter2D (Collision2D col) {
			CollisionEventArgs eventArgs = new CollisionEventArgs ();
			eventArgs.Target = this;
			MessageQueue.dispatchEvent (this, eventArgs);
		}

	}

}