const { app, BrowserWindow } = require('electron')

let win

app.on('ready', _ => {
  win = new BrowserWindow({
    width: 800,
    height: 600
  })

  win.loadURL('https://10.86.5.107/#/log-in')
  win.webContents.openDevTools()
})

app.commandLine.appendSwitch('ignore-certificate-errors');
// const path = require('path')

// function createWindow () {
//   const win = new BrowserWindow({
//     width: 800,
//     height: 600,
//     // webPreferences: {
//     //   preload: path.join(__dirname, 'preload.js')
//     // }
//   })

//   // win.loadFile('index.html')
//   win.loadURL('https://10.86.5.107/#/log-in')
// }

// app.whenReady().then(() => {
//   createWindow()

//   app.on('activate', () => {
//     if (BrowserWindow.getAllWindows().length === 0) {
//       createWindow()
//     }
//   })
// })

// app.on('window-all-closed', () => {
//   if (process.platform !== 'darwin') {
//     app.quit()
//   }
// })
