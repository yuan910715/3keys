#ifndef __USB_HID_KBD_H__
#define __USB_HID_KBD_H__

#include <stdint.h>
#include "include/ch5xx.h"
#include "include/ch5xx_usb.h"


#ifdef __cplusplus
extern "C" {
#endif

uint8_t Keyboard_press(__data uint8_t k);
uint8_t Keyboard_release(__data uint8_t k);
void Keyboard_releaseAll(void);

uint8_t Keyboard_write(__data uint8_t c);
uint8_t Consumer_press(__data uint16_t k);
uint8_t Consumer_release(__data uint16_t k);


uint8_t Mouse_press(__data uint8_t k);
uint8_t Mouse_release(__data uint8_t k);
uint8_t Mouse_click(__data uint8_t k);
uint8_t Mouse_move(__data int8_t x,__data int8_t y);
uint8_t Mouse_scroll(__data int8_t tilt);

#ifdef __cplusplus
} // extern "C"
#endif

#endif

