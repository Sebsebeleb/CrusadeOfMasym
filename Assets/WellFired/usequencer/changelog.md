\page changelog Changelog

\tableofcontents

\section latest Latest

\subsection v139 1.3.9
Version 1.3.9 provides usability enhancements and a bunch of great new features including an improved observer timeline with transitions and object path timeline with handycam support

\subsubsection v139features Features
- Updated all documentation (online / offline)
- Added Handycam / shake support to object path timelines.
- Added Camera transitions to the observer timeline. (Unity Pro Only).
- Added support for Rotation (Euler).
- Added support for Mecanim Custom rigs to the Animation Timeline.
- Improved Inspector for sequences, allowing you to create / update prefabs and duplicate sequences as well as open uSequencer directly from the inspector. (Works for multiple objects at once)
- Improved Inspector for Object Path Timeline, allows for aligning of keyframes to objects
- Improved Inspector for Object Path Timeline, allows for the setting of ease and shake settings.
- Improved Inspector for Observer Keyframes, allows for the setting of time, cameras and transitions (Unity Pro Only).
- Improved Inspector for Spline Keyframes, allows for the aligning of keyframes to objects in the scene view.
- Added Late binding callbacks to uSequencer Timelines, so you can perform custom operations on Late Binding
- Skip to next / prev keyframe now only works on visible keyframes.
- Added timeline hierarchy sorting and sorted position / rotation to first show local, will also work with custom timelines.
- Added a rotate object event.
- Added another couple of Playmaker events. (Improved existing ones).
- Added Unity UI Intergration
- Unity 5.0 support (Email us for this at support@wellfired.com)

\subsubsection v139fixes Fixes
- Fix for some object references being lost when saving prefabs.
- Fixed Undo / Redo support for duplication of prefabs.
- Sometimes Object Path Timelines were not serialized / deserialized correctly.
- Fix for sometimes HotKeys wouldn't work when another Unity control had focus.
- Fix for selecting a scrolled Animation window item.

\section older Older

\subsection v138 1.3.8
Version 1.3.8 provides usability enhancements and a bunch of great new features including Unity 4.6 UI support and Unity 5.0 support

\subsubsection v138features Features
- We've Updated our welcome screen to point to new web pages etc
- Added Unity UI support for Unity 4.6 (new components : PlaySequence, PauseSequencer, SkipSequence, StopSequence, SequencePreviewer)
- Added Unity 5.0 support, contact us for this.
- Added a new Event Stop And Skip To time
- Added a bunch of new Send Message Events that take various parameters
- Added a (beta / pro only) Dissolve Transition 
- Added a small bit of information describing what Observer Timelines are doing
- Observer Timelines now toggle Audio Listeners
- We now Expose FireOnSkip so users can modify this through code.
- Added In-Depth Documentation and Updated it with any new changes

\subsubsection v138fixes Fixes
- We now only accept inspector input that is valid to resolve the issue with settings keyframes at 0.x
- We now update the curve rendering when the user changes the duration
- Sometimes Skipping a sequence would not work
- Now we don't display user removed components, this stops us from getting into an unrecoverable null reference state
- Fix a bug that would stop saving from working correctly in Unity 4.6.0p1

\subsection v137 1.3.7
Version 1.3.7 provides usability enhancements and a bunch of great new features (Copy / Paste Anyone! :D)

\subsubsection v137features Features
- Added Copy / Paste Suport for every single object in uSequencer.
- Update our Welcome page to point to correct websites and ensure the PDF documentation opens correctly.
- Updated the functionality of Observer Timelines, so we only turn off the Camera, improving performance.
- Custom Timelines now have the ability to extend the context menu through the AddContextItems method.
- Added an Enable / Disable Component Event.
- Added a piece of UI that should function in the same way as the context click on timelines.
- Added rootMotion to our Animation Timeline Beta
- We no longer hide duration on PlayAnimationEvents
- Added quick Sequence selection, from within the uSequencer UI.
- Added Animation Timelines publically as the Beta exits Alpha stage.

\subsubsection v137fixes Fixes
- Several fixes for Undo / Redo.
- Several fixes for random null reference issues.
- Bug fixes for fighting on looping sequences when an event / keyframe was at 0.0f
- Bug fixes for the Look At Object Event editor not displaying correctly.
- Bug fixes for ensuring the Object Path Timeline will always be reset under each different circumstance.
- Bug fixes for ensuring we can create Prefabs out of Animation Timelines (beta).
- Bug fixes for Windows App Store submission.
- Bug fixed for ensuring events are actually undone in the correct order.

\subsection v1364 1.3.6.4
Version 1.3.6.4 provides usability enhancements

\subsubsection v1364features Features
- Now you can animate properties from their existing values, rather than defined values, to activate this, context click on the property in the hierarchy pane and select Use Current Value. Current value keyframes will show up as red when not selected.
- We've added quick selection to the UI for uSequencer (Thanks to for the pointer Jean-François Pérusse).
- New quiet flag when Building sequences with the USEditorUtility.CreatePrefabFrom method, you can use this if you want to create tools to integrate with uSequencer.
- Added an IsComplete Property to sequences.
- Added two utility methods on USSequencer objects USSequencer.HasTimelineContainerFor and USSequencer.GetTimelineContainerFor

\subsubsection v1364fixes Fixes
- Fixed a bug where the bounds wouldn't be updated when adding keyframes.

\subsection v1363 1.3.6.3
Version 1.3.6.3 includes some project clean up ability, removing the need to think about old files in your project when using prefabs with uSequencer as well as adding support manipulating multiple keyframes on a single curve on a property (Auto adding keys to only X on an Vector3 for instance). As well as building upon our frameworks and laying the foundations for Animation Timelines.

\subsubsection v1363features Features
- We now cleanup unused files when you Update a prefab - No More Project Clutter.
- Added a Set Affected Object to selection option within the uSequencer window. (This works through context clicking)
- Added the ability for the manipulation of single curves, you can now, for instance edit the X,Y,Z Curve of a Vector3 independantly, when the other curves are hidden we no longer add keyframes to those curves.
- Hide Duration on events that don't need to display duration, this is to avoid confusion. If you want this functionality for your own custom events, add the following attribute [USequencerEventHideDuration()]
- Added a tool to combine Audio Events into a single Wav File.
- Put in place a Framework for Animation Timelines.
- Allow for the Undo / Redo Handles in the scene view handles with curve properties.

\subsubsection v1363fixes Fixes
- Sometimes the free skin would be shown in place of the pro skin and vice versa.
- When an Affected Object is no longer in the scene, we will inform the user in the UI. This is no longer a breaking change.
- Adding a keyframe would sometimes break the XScale of your curves in uSequencer.
- Curves would sometimes appear to be missing, however the timeline would still play back.
- Renamed the MouseLook.cs class so that Unity's built in script still works.
- Renamed the USTimeScaleEvent.cs file to match the class name. This allows unity to serialize data correctly.

\subsection v1362 1.3.6.2
Version 1.3.6.2 saw us add the ability to duplicate (copy and paste) keyframes, events, oberver keyframes in our own way, we also added a bunch of user preferences to make uSequencer easier to work with certain users.

\subsubsection v1362Features Features
- Added Keyframe / Event Duplication functionality to uSequencer, hold down <kbd>ctrl</kbd> and <kbd>alt</kbd> when you start dragging to duplicate your selection. (Works with everything)
- Added a Render Hierarchy Gizmos preference.
- Added an invert zoom direction preference.
- Added in-comment documentation that is parsed by MonoDevelop and Visual Studio.
- Update our Manual and Welcome Screen to our new Homepage.

\subsubsection v1362Fixes Fixes
- Keyframe handles now render with the correct tangent to the curve.
- Fix for issue with Observer Timelines resetting cameras when the timeliens have no keyframes.
- Fixed small graphical glitch with our vertical scroll bar.
- Ensure that uSequencer compiles and runs correctly, complying completely with Win 8 WACK tests.