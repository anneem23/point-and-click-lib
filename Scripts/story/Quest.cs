// ------------------------------------------------------------------------------
//  Trigger
//
//	Something beyond the control of the protagonist (hero/heroine) is the trigger which sparks 
//	off the story. A fairy godmother appears, someone pays in magic beans not gold, a mysterious 
//	letter arrives … you get the picture.
//
//	The quest
//		
//  The trigger results in a quest – an unpleasant trigger (e.g. a protagonist losing his job) 
//  might involve a quest to return to the status quo; a pleasant trigger (e.g. finding a treasure map) 
//	means a quest to maintain or increase the new pleasant state.
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections;

namespace PointAndClick.Story {
	public class QuestEventArgs : System.EventArgs {
		public Quest Quest { get; set; }
	}

	public class Quest {
		// contains rules for trigger to start quest
		private Func<object, bool> preCondition;
		// contains rules for trigger to end quest
		private Func<object, bool> postCondition;

		private String name;
		//private List<Actionable> actionables;

		public Quest (String name, 
					  Func<object, bool> preCondition, 
					  Func<object, bool> postCondition) {
			this.name = name;
			this.preCondition = preCondition;
			this.postCondition = postCondition;
		}

		public bool isUnlocked(object t) {
			return preCondition != null ? preCondition (t) : false;
		}
		
		public bool isCompleted(object t) {
			return postCondition != null ? postCondition (t) : false;
		}

		public String Name {
			get {
				return name;
			}
		}
	}
}