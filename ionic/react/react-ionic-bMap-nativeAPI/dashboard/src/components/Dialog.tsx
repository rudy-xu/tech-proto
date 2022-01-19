import React, { useEffect, useState } from "react";
import "./Dialog.css";
/**
 * One Dialog include:
 *  1.The title area
 *  2.The content area
 *  3.The control area
 *  4.One mask
 *  Base Function:
 *      1.control model to show and hidden with visible
 *      2.show content
 *      3.click "Cancel"  and  "ok"to callback onClose and confirm to operate modal
 *      4.click "mask" to close modal
 *      5.animate field to open/close animation
 */
interface dialogParas{
    visible: boolean;
    closeModal: Function;
}

const Dialog: React.FC<dialogParas> = ({ visible, closeModal }) => {
    // console.log("Dialog");

    const [locVisible, setLocVisible] =useState(false);
    const [classes, setClasses] = useState<string>("");

    useEffect(()=>{
        setLocVisible(visible);
        // enterAnimate();
    },[visible]);

    function enterAnimate(): void{
        const enterClasses = "modal-enter";
        const enterActiveClasses = "modal-enter-active";
        const enterEndActiveClasses = "modal-enter-end";

        const enterTimeout = 0;
        const enterActiveTimeout = 200;
        const enterEndTimeout = 100;

        setClasses(enterClasses);

        const enterActiveTimer = setTimeout(() => {
            setClasses(enterActiveClasses);
            clearTimeout(enterActiveTimer);
        },enterTimeout);

        const enterEndTimer = setTimeout(() => {
            setClasses(enterEndActiveClasses);
            clearTimeout(enterEndTimer);
        },enterTimeout+enterActiveTimeout);

        const initTimer = setTimeout(() => {
            setClasses("");
            clearTimeout(initTimer);
        },enterTimeout+enterActiveTimeout+enterEndTimeout);
    }

    function leaveAnimate(): void{
        const leaveClasses = 'modal-leave'
        const leaveActiveClasses = 'modal-leave-active'
        const leaveEndActiveClasses = 'modal-leave-end'
        const leaveTimeout = 0
        const leaveActiveTimeout = 100
        const leaveEndTimeout = 200

        setClasses(leaveClasses);

        const leaveActiveTimer = setTimeout(() => {
            setClasses(leaveActiveClasses);
            clearTimeout(leaveActiveTimer);
        },leaveTimeout);

        const leaveEndTimer = setTimeout(() => {
            setClasses(leaveEndActiveClasses);
            clearTimeout(leaveEndTimer);
        },leaveTimeout+ leaveActiveTimeout);

        const initTimer = setTimeout(() => {
            setClasses("");
            clearTimeout(initTimer);
        },leaveTimeout+leaveActiveTimeout+leaveEndTimeout);
    }

    function handleOk(): void {
        console.log("My name is OK");
        setLocVisible(false);
        closeModal();
    }

    function handleCancel(): void {
        console.log("My name is Cancel");
        setLocVisible(true);
    }

    function handleMask(): void {
        console.log("My name is Mask");
        setLocVisible(false);
        closeModal();
    }

    return (
        <>
            { !locVisible ? null :
                <div className="modal-wrapper">
                    {/* <div className="modal"> */}
                    <div className={`modal ${classes}`}>
                        <div className="modal-title"> This is title</div>
                        <div className="modal-content"> This is content</div>
                        <div className="modal-operator">
                            <button className="modal-operator-close" onClick={() => { handleCancel() }}>Cancel</button>
                            <button className="modal-operator-confirm" onClick={() => { handleOk() }}>Ok</button>
                        </div>
                    </div>
                    {/* <div className="mask" onClick={() => { handleMask() }}></div> */}
                </div>
            }
        </>
    );
};

export default Dialog;