![picture](https://drive.google.com/uc?id=1CmGvIAMUEN4d0KFqcOwp2aoG_xU9h1KR)

Thanks for playing bloqit!

This tiny qubit duel takes:

* **2 players**: a *Sender* and a *Blocker*
* **1 computer** to pass between you

Bloqit is run on a **quantum circuit**. To create our quantum circuit with three qubits (q0, q1, and q2), we're running ```circ = QuantumCircuit(3)``` and then adding gates to put the quibits in a superposition.

```
circ = QuantumCircuit(3)

# Add a H gate on qubit 0, putting this qubit in superposition.
circ.h(0)
# Add a H gate on qubit 1, putting this qubit in superposition. 
circ.h(1)
# Add a H gate on qubit 2, putting this qubit in superposition. 
circ.h(2)
```

We can visualize this circuit by running ```circ.draw()```

In bloqit, the:
- **Sender** player tries make as many qubits equal **1** when we measure 
- **Blocker** player tries to make as many qubits equal **0** when we measure <br>

To send or block, each player has a certain number of **gates** they play. These include:
- **CX gate**: does a 180 degree rotation about the x axis if the control qbit is 1. Also known as CNOT
- **CZ gate**: does a 180 degree rotation about the Z axis if the control qbit is 1
- **H gate**: does a 90 degree rotation in an axis that is 45 degrees from the z and y axe's
- **S gate**: does a 90 degree rotation about the Z axis
- **SWAP**: swaps the values of 2 qbits
- **X gate** : does a 180 degree rotation in the x axis
- **Y gate**: does 180 degree rotation in the y axis
- **Z gate**: does a 180 degree rotation in the Z axis

Once you have both used your gates, it's time to **measure**.

To measure, we generate a bitstring that results after making independent measurements. The bitstring ``` x y z ``` corresponds to qubit 2, qubit 1, and qubit 0. When we make a measurement:
- A **quantum circuit** ```meas``` is created to get the quantum meaurement. 
- The **barrier** ```barrier``` helps us distinguish the measurement part of the circuit. 
- The outcome of the qubits are **mapped to classical bits** ```c_0, c_1, c_2``` when we run ```meas.measure(range(3), range(3))```. 
- To visualize everything, we create ```qc```

Play around with our code with colab [here!](https://colab.research.google.com/drive/1nXmAMXcMyuAs2Zd9yBN-gX_5mrVIZuzS#scrollTo=_kQyhVuvO33P)
