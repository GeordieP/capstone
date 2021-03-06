using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
    // States
    private States state = States.Idle;
    private enum States {
        Idle,
        Walking,
        Working
    }

    // Nodes
    private Transform currentTarget;
    private List<Transform> targetNodes;

    // Task
    private UnitTask currentTask;

	// Resource
	private ResourcePackage carriedResource;

    // Unit
    public bool selected = false;
	public Renderer[] renderers;

	// Unit Movement
    private float speed = 4.0f;

    // Use this for initialization
    void Start() {
        targetNodes = new List<Transform>();
		carriedResource = new ResourcePackage();
		renderers = GetComponentsInChildren<Renderer>();
    }

	void Update () {
		switch (state) {
			case States.Idle:
				break;
			case States.Walking:
				if (currentTarget == null) {
					targetNodes.Clear();
					ChangeState(States.Idle);
					break;
				}
				transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
				break;
		case States.Working:
				break;
			default:
				break;
		}
	}

	// Begin executing the task provided by the node we're at
	void StartTask() {
		currentTask = currentTarget.GetComponent<Node>().GetTask();
		StartCoroutine(WaitForTaskTime());
	}

	// Wait for some time before completing the task (this is the "work" period)
	IEnumerator WaitForTaskTime() {
		yield return new WaitForSeconds(currentTask.taskTime);
		TaskCompleted();
	}

	// Work period has gone by, now we can call TaskCompleted on our current task, and move on to the next target in the queue
	public void TaskCompleted() {
		if (currentTarget != null)
			currentTask.TaskCompleted(this, currentTarget.GetComponent<Node>());
		NextTarget();
	}

	// Accept a node's ResourcePackage -- this should only ever get called from a GatherTask
	public void AcceptResourcePackage(ResourcePackage _resourcePackage) {
		carriedResource = _resourcePackage;
	}

	// Take our carried ResourcePackage, so the UnitTask can give it to the city -- should only ever get called from a DepositTask
	public ResourcePackage TakeResourcePackage() {
		// back up and then reset the unit's carried resource package, then return the backup
		ResourcePackage packageToReturn = carriedResource;
		carriedResource = new ResourcePackage();
		return packageToReturn;
	}

    private void NextTarget() {
        ChangeState(States.Idle);
        targetNodes.RemoveAt(0);

        if (targetNodes.Count > 0) {
			if (targetNodes[0] != null) {
				SetTarget(targetNodes[0]);
				ChangeState(States.Walking);
			}
        }	// else no more nodes left in the list
    }

    public void AddTarget(Transform _target) {
        // HACK: this is a shit way to do this
		// only add the target if the queue is empty, or the target isn't the same as the last node that was added
		if (targetNodes.Count < 1 || _target != targetNodes[targetNodes.Count - 1]) {
			targetNodes.Add(_target);
		}

        if (state == States.Idle) {
            SetTarget(targetNodes[0]);
            ChangeState(States.Walking);
        }
    }

    public void SetTarget(Transform _target) {
        currentTarget = _target;

        if (state == States.Idle) {
            ChangeState(States.Walking);
        }
    }

    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.gameObject.transform == currentTarget) {
            // we've made it to the target, start working
			ChangeState(States.Working);
			StartTask();
        }

        else if (otherCollider.gameObject.transform != currentTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, otherCollider.transform.position, -0.1f);
        }
    }

    /* Called by the camera when selecting units. Prevents selecting units that shouldn't be
        selectable gets updated by the camera and should only be true when the unit is visible to the player */
    public bool Select() {
		renderers [1].enabled = true;
        selected = true;
        return true;
    }

    public void Deselect() {
		renderers [1].enabled = false;
        selected = false;
    }

    // Helper function for state changing so in the future we can make things happen when the state is changed if we need to (also for debugging)
    void ChangeState(States state) {
        this.state = state;
    }
}
